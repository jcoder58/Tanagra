﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Tanagra.Generator
{
    public class CSharpSpecRewriter
    {
        Dictionary<string, string> basetypeOverride;
        Dictionary<string, string> structNameOverride;

        // TODO: read this info from the constant struct
        Dictionary<string, string> constantMap;

        public CSharpSpecRewriter()
        {
            basetypeOverride = new Dictionary<string, string>
            {
                { "VkSampleMask", "uint32_t" },
                //{ "VkBool32", "uint32_t" },
                //{ "VkFlags", "uint32_t" },
                { "VkDeviceSize", "uint64_t" },
            };
            
            structNameOverride = new Dictionary<string, string>
            {
                { "void",     "IntPtr" },
                { "char",     "String" },
                { "float",    "Single" },
                { "uint8_t",  "Byte"   },
                { "uint32_t", "UInt32" },
                { "uint64_t", "UInt64" },
                { "int32_t",  "Int32"  },
                { "size_t",   "UInt32" },
                { "VkBool32", "Bool32" }
            };

            constantMap = new Dictionary<string, string>
            {
                { "VK_MAX_PHYSICAL_DEVICE_NAME_SIZE", "256" },
                { "VK_UUID_SIZE",                      "16" },
                { "VK_MAX_EXTENSION_NAME_SIZE",       "256" },
                { "VK_MAX_DESCRIPTION_SIZE",          "256" },
                { "VK_MAX_MEMORY_TYPES",               "32" },
                { "VK_MAX_MEMORY_HEAPS",               "16" },
            };
        }

        public VkSpec Rewrite(VkSpec spec)
        {
            //
            // `CSharpSpecRewriter` takes a spec generated by `VKSpecReader` and prepares
            // it for C# code generation. Remember that all the type data is linked by `VkType`
            // objects, so changing the name of a struct will change the name in any other structs
            // and all commands where it is referenced.
            //

            MergeExtensionEnums(spec.Enums.ToArray(), spec.Extensions);

            var apiConstants = spec.Enums.First(x => x.Name == "API Constants");
            MergeExtensionConstants(apiConstants, spec.Extensions);

            foreach(var vkEnum in spec.Enums)
                RewriteEnumDefinition(vkEnum);

            foreach(var vkHandle in spec.Handles)
                RewriteHandleDefinition(vkHandle);

            foreach(var vkStruct in spec.Structs)
                RewriteStructDefinition(vkStruct);

            foreach (var vkStruct in spec.Structs)
                RewriteStructMemberLen(vkStruct, spec.Structs.ToArray());

            foreach (var vkCmd in spec.Commands)
                RewriteCommandDefinition(vkCmd);

            foreach(var vkCmd in spec.Commands)
                RewriteCommandParamLen(vkCmd, spec.Structs.ToArray());

            // Replace all imported type refrences with IntPtr
            var intPtr = spec.AllTypes.FirstOrDefault(x => x.Name == "IntPtr");

            var platfromTypes = spec.AllTypes.Where(x => x.IsImportedType);
            foreach(var vkType in platfromTypes)
                Replace(spec, vkType, intPtr);

            spec.AllTypes = spec.AllTypes.Except(platfromTypes).ToList();

            var functionPointers = spec.AllTypes.Where(x => x.Name.StartsWith("PFN_"));
            foreach(var vkType in functionPointers)
                Replace(spec, vkType, intPtr);

            spec.AllTypes = spec.AllTypes.Except(functionPointers).ToList();
            
            return spec;
        }
        
        void RewriteHandleDefinition(VkHandle vkHandle)
        {
            if(vkHandle.Name.StartsWith("Vk"))
                vkHandle.Name = vkHandle.Name.Remove(0, 2); // trim `Vk`
        }

        void RewriteStructDefinition(VkStruct vkStruct)
        {
            var name = vkStruct.Name;

            if(structNameOverride.ContainsKey(name))
                name = structNameOverride[name];

            if(name.StartsWith("Vk"))
                name = name.Remove(0, 2); // trim `Vk`

            vkStruct.Name = name;

            for(var x = 0; x < vkStruct.Members.Length; x++)
            {
                var member = vkStruct.Members[x];
                var memberName = member.Name;
                if(member.PointerRank != 0)
                    memberName = memberName.TrimStart(new[] { 'p' });

                if(memberName.StartsWith("pfn"))
                    memberName = memberName.Remove(0, 3);

                memberName = char.ToUpper(memberName[0]) + memberName.Substring(1);
                member.Name = memberName;

                if (member.IsFixedSize)
                {
                    if (constantMap.ContainsKey(member.FixedSize))
                        member.FixedSize = constantMap[member.FixedSize];
                }

                if(!string.IsNullOrEmpty(member.XMLComment))
                {
                    var comment = member.XMLComment;
                    comment = char.ToUpper(comment[0]) + comment.Substring(1, comment.Length - 1);
                    comment = comment.Replace("\r", string.Empty);
                    comment = comment.Replace("\n", " ");
                    
                    Regex regex = new Regex("[ ]{2,}", RegexOptions.None);
                    comment = regex.Replace(comment, " ");

                    member.XMLComment = comment;
                }
            }
        }

        void RewriteStructMemberLen(VkStruct vkStruct, VkStruct[] allStructs)
        {
            for (var x = 0; x < vkStruct.Members.Length; x++)
            {
                var member = vkStruct.Members[x];
                if (member.Len.Length > 0)
                {
                    if (member.Len[0] == @"latexmath:[$codeSize \over 4$]" && member.Type.Name == "UInt32")
                    {
                        member.Len[0] = "codeSize";
                        member.Type = allStructs.First(y => y.Name == "Byte");
                    }
                    member.Len[0] = ToFirstUppercase(member.Len[0]);
                }
            }
        }

        void MergeExtensionEnums(VkEnum[] specEnums, VkExtension[] extensions)
        {
            foreach (var specEnum in specEnums)
            {
                foreach (var ext in extensions.Where(x => x.Supported != "disabled"))
                {
                    var extNumber = ext.Number - 1;

                    var deltas = ext
                        .Requirement.Enums
                        .Where(x => !x.IsConstant && x.Extends == specEnum.Name);

                    if (deltas.Any())
                    {
                        var existing = specEnum.Values.ToList();
                        foreach (var newEnumValue in deltas)
                        {
                            if (newEnumValue.IsFlag)
                            {
                                // ... todo
                            }
                            else
                            {
                                if(!newEnumValue.Offset.HasValue)
                                    continue;

                                var enumValue = 1000000000;
                                enumValue += 1000 * extNumber;
                                enumValue += newEnumValue.Offset.Value;
                                if (newEnumValue.Dir == "-")
                                    enumValue *= -1;

                                VkEnumValue vkEnumValue = new VkEnumValue();
                                vkEnumValue.Name = vkEnumValue.SpecName = newEnumValue.Name;
                                vkEnumValue.Comment = newEnumValue.Comment;
                                vkEnumValue.Value = enumValue.ToString();

                                existing.Add(vkEnumValue);
                            }
                        }
                        specEnum.Values = existing.ToArray();
                    }
                }
            }
        }

        void MergeExtensionConstants(VkEnum apiConstants, VkExtension[] extensions)
        {
            var constantList = apiConstants.Values.ToList();
            foreach(var ext in extensions.Where(x => x.Supported != "disabled"))
            {
                var constants = ext.Requirement.Enums.Where(x => x.IsConstant);
                foreach(var newConstantValue in constants)
                {
                    constantList.Add(new VkEnumValue {
                        Name     = newConstantValue.Name,
                        SpecName = newConstantValue.Name,
                        Value    = newConstantValue.Value.Replace("\"", string.Empty),
                    });
                }
            }
            apiConstants.Values = constantList.ToArray();
        }

        void RewriteEnumDefinition(VkEnum vkEnum)
        {
            if(vkEnum.Name.StartsWith("Vk"))
                vkEnum.Name = vkEnum.Name.Remove(0, 2); // trim `Vk`
            
            var isFlags = vkEnum.Name.EndsWith("Flags");

            var trimKHR = false;
            var trimEXT = false;

            var enumPrefix = vkEnum.Name;
            if (enumPrefix.EndsWith("KHR"))
            {
                enumPrefix = enumPrefix.Remove(enumPrefix.Length - 3, 3); // trim `KHR`
                trimKHR = true;
            }

            if (enumPrefix.EndsWith("EXT"))
            {
                enumPrefix = enumPrefix.Remove(enumPrefix.Length - 3, 3); // trim `EXT`
                trimEXT = true;
            }

            //if(isFlags)
            if (enumPrefix.EndsWith("Flags"))
                enumPrefix = enumPrefix.Substring(0, enumPrefix.Length - 5);

            enumPrefix = ToUppercaseEnumName(enumPrefix) + "_";
            
            var firstValue = vkEnum.Values.First();
            
            foreach(var vkEnumValue in vkEnum.Values)
            {
                var name = vkEnumValue.Name;
                if(name.StartsWith("VK_"))
                    name = name.Substring(3, name.Length - 3);

                if(!string.IsNullOrEmpty(enumPrefix) && name.StartsWith(enumPrefix))
                    name = name.Substring(enumPrefix.Length, name.Length - enumPrefix.Length);
                
                // bleh, this is janky

                if(name.EndsWith("_KHR") && trimKHR)
                    name = name.Remove(name.Length - 4, 4); // trim `_KHR`

                if (name.EndsWith("_EXT") && trimEXT)
                    name = name.Remove(name.Length - 4, 4); // trim `_KHR`
                
                if (name.EndsWith("_BIT"))
                    name = name.Substring(0, name.Length - 4);
                
                name = ToCamelCaseEnumName(name);

                if (name.EndsWith("Khr"))
                    name = name.Remove(name.Length - 3, 3) + "KHR";

                if (name.EndsWith("Ext"))
                    name = name.Remove(name.Length - 3, 3) + "EXT";

                if (name.EndsWith("Amd"))
                    name = name.Remove(name.Length - 3, 3) + "AMD";

                vkEnumValue.Name = name;
            }

            // After the we've renamed all the enum values, check if there are any that
            // begin with a number (invalid in C#) and fix it.
            if(vkEnum.Values.Any(x => Regex.IsMatch(x.Name, "^[0-9]")))
            {
                foreach(var vkEnumValue in vkEnum.Values)
                {
                    var name = vkEnumValue.Name;
                    name = vkEnum.Name + name;
                    vkEnumValue.Name = name;
                }
            }
        }

        string ToUppercaseEnumName(string name)
        {
            var newName = string.Empty;
            for(var x = 0; x < name.Length; x++)
            {
                if(char.IsUpper(name[x]) && x != 0)
                {
                    newName += "_" + name[x];
                }
                else
                {
                    newName += char.ToUpper(name[x]);
                }
            }
            return newName;
        }

        string ToCamelCaseEnumName(string name)
        {
            var newName = string.Empty;
            for(var x = 0; x < name.Length; x++)
            {
                if(name[x] == '_')
                {
                    newName += name[x + 1];
                    x++;
                }
                else
                {
                    if(x == 0)
                    {
                        newName += name[x];
                    }
                    else
                    {
                        newName += char.ToLower(name[x]);
                    }
                }
            }
            return newName;
        }
        
        void RewriteCommandDefinition(VkCommand vkCommand)
        {
            if(vkCommand.Name.StartsWith("vk"))
                vkCommand.Name = vkCommand.Name.Remove(0, 2); // trim `Vk`

            for(var x = 0; x < vkCommand.Parameters.Length; x++)
            {
                var param = vkCommand.Parameters[x];
                var paramName = param.Name;
                
                if(param.IsPointer)
                    paramName = paramName.TrimStart(new[] { 'p' });

                paramName = char.ToLower(paramName[0]) + paramName.Substring(1);

                if (paramName == "event" || paramName == "object")
                    paramName = '@' + paramName; // alias names

                param.Name = paramName;

                if(!string.IsNullOrEmpty(param.Len))
                {
                    var lenName = param.Len;
                    lenName = lenName.TrimStart(new[] { 'p' });
                    lenName = char.ToLower(lenName[0]) + lenName.Substring(1);
                    param.Len = lenName;
                }
            }

            if(vkCommand.CmdBufferLevel.Length != 0)
            {
                for(int x = 0; x < vkCommand.CmdBufferLevel.Length; x++)
                {
                    var str = vkCommand.CmdBufferLevel[x];
                    str = char.ToUpper(str[0]) + str.Substring(1, str.Length - 1);
                    vkCommand.CmdBufferLevel[x] = str;
                }
            }

            if(!string.IsNullOrEmpty(vkCommand.RenderPass))
                vkCommand.RenderPass = char.ToUpper(vkCommand.RenderPass[0]) + vkCommand.RenderPass.Substring(1, vkCommand.RenderPass.Length - 1);

            if(vkCommand.Queues.Length != 0)
            {
                for(int x = 0; x < vkCommand.Queues.Length; x++)
                {
                    var str = vkCommand.Queues[x];
                    str = char.ToUpper(str[0]) + str.Substring(1, str.Length - 1);
                    vkCommand.Queues[x] = str;
                }
            }
        }

        void RewriteCommandParamLen(VkCommand vkCommand, VkStruct[] allStructs)
        {
            for(var x = 0; x < vkCommand.Parameters.Length; x++)
            {
                var param = vkCommand.Parameters[x];
                if(param.Len == @"latexmath:[$dataSize \over 4$]" && param.Type.Name == "UInt32")
                {
                    param.Len = "dataSize";
                    param.Type = allStructs.First(y => y.Name == "Byte");
                }
            }
        }

        void Replace(VkSpec spec, VkType existingType, VkType newType)
        {
            var vkStructs = spec.Structs.ToList();
            for(int x = 0; x < vkStructs.Count; x++)
            {
                var vkStruct = vkStructs[x];
                for(int y = 0; y < vkStruct.Members.Length; y++)
                {
                    var vkParam = vkStruct.Members[y];
                    if(vkParam.Type == existingType)
                        vkParam.Type = newType;
                }
            }

            var vkCommands = spec.Commands;
            for(int x = 0; x < vkCommands.Length; x++)
            {
                var vkCommand = vkCommands[x];
                for(int y = 0; y < vkCommand.Parameters.Length; y++)
                {
                    var vkParam = vkCommand.Parameters[y];
                    if(vkParam.Type == existingType)
                        vkParam.Type = newType;
                }

                if(vkCommand.ReturnType == existingType)
                    vkCommand.ReturnType = newType;
            }
        }

        string ToFirstUppercase(string str)
            => char.ToUpper(str[0]) + str.Substring(1, str.Length - 1);
    }
}
