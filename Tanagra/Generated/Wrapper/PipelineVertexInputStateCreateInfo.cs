using System;
using System.Runtime.InteropServices;

namespace Vulkan
{
    unsafe public class PipelineVertexInputStateCreateInfo
    {
        internal Interop.PipelineVertexInputStateCreateInfo* NativePointer;
        
        /// <summary>
        /// Reserved (Optional)
        /// </summary>
        public PipelineVertexInputStateCreateFlags Flags
        {
            get { return NativePointer->Flags; }
            set { NativePointer->Flags = value; }
        }
        
        public VertexInputBindingDescription[] VertexBindingDescriptions
        {
            get
            {
                if(NativePointer->VertexBindingDescriptions == IntPtr.Zero)
                    return null;
                var valueCount = NativePointer->VertexBindingDescriptionCount;
                var valueArray = new VertexInputBindingDescription[valueCount];
                var ptr = (VertexInputBindingDescription*)NativePointer->VertexBindingDescriptions;
                for(var x = 0; x < valueCount; x++)
                    valueArray[x] = ptr[x];
                
                return valueArray;
            }
            set
            {
                if(value != null)
                {
                    var valueCount = value.Length;
                    var typeSize = Marshal.SizeOf<VertexInputBindingDescription>() * valueCount;
                    if(NativePointer->VertexBindingDescriptions != IntPtr.Zero)
                        Marshal.ReAllocHGlobal(NativePointer->VertexBindingDescriptions, (IntPtr)typeSize);
                    
                    if(NativePointer->VertexBindingDescriptions == IntPtr.Zero)
                        NativePointer->VertexBindingDescriptions = Marshal.AllocHGlobal(typeSize);
                    
                    NativePointer->VertexBindingDescriptionCount = (UInt32)valueCount;
                    var ptr = (VertexInputBindingDescription*)NativePointer->VertexBindingDescriptions;
                    for(var x = 0; x < valueCount; x++)
                        ptr[x] = value[x];
                }
                else
                {
                    if(NativePointer->VertexBindingDescriptions != IntPtr.Zero)
                        Marshal.FreeHGlobal(NativePointer->VertexBindingDescriptions);
                    
                    NativePointer->VertexBindingDescriptions = IntPtr.Zero;
                    NativePointer->VertexBindingDescriptionCount = 0;
                }
            }
        }
        
        public VertexInputAttributeDescription[] VertexAttributeDescriptions
        {
            get
            {
                if(NativePointer->VertexAttributeDescriptions == IntPtr.Zero)
                    return null;
                var valueCount = NativePointer->VertexAttributeDescriptionCount;
                var valueArray = new VertexInputAttributeDescription[valueCount];
                var ptr = (VertexInputAttributeDescription*)NativePointer->VertexAttributeDescriptions;
                for(var x = 0; x < valueCount; x++)
                    valueArray[x] = ptr[x];
                
                return valueArray;
            }
            set
            {
                if(value != null)
                {
                    var valueCount = value.Length;
                    var typeSize = Marshal.SizeOf<VertexInputAttributeDescription>() * valueCount;
                    if(NativePointer->VertexAttributeDescriptions != IntPtr.Zero)
                        Marshal.ReAllocHGlobal(NativePointer->VertexAttributeDescriptions, (IntPtr)typeSize);
                    
                    if(NativePointer->VertexAttributeDescriptions == IntPtr.Zero)
                        NativePointer->VertexAttributeDescriptions = Marshal.AllocHGlobal(typeSize);
                    
                    NativePointer->VertexAttributeDescriptionCount = (UInt32)valueCount;
                    var ptr = (VertexInputAttributeDescription*)NativePointer->VertexAttributeDescriptions;
                    for(var x = 0; x < valueCount; x++)
                        ptr[x] = value[x];
                }
                else
                {
                    if(NativePointer->VertexAttributeDescriptions != IntPtr.Zero)
                        Marshal.FreeHGlobal(NativePointer->VertexAttributeDescriptions);
                    
                    NativePointer->VertexAttributeDescriptions = IntPtr.Zero;
                    NativePointer->VertexAttributeDescriptionCount = 0;
                }
            }
        }
        
        public PipelineVertexInputStateCreateInfo()
        {
            NativePointer = (Interop.PipelineVertexInputStateCreateInfo*)Interop.Structure.Allocate(typeof(Interop.PipelineVertexInputStateCreateInfo));
            NativePointer->SType = StructureType.PipelineVertexInputStateCreateInfo;
        }
        
        public PipelineVertexInputStateCreateInfo(VertexInputBindingDescription[] VertexBindingDescriptions, VertexInputAttributeDescription[] VertexAttributeDescriptions) : this()
        {
            this.VertexBindingDescriptions = VertexBindingDescriptions;
            this.VertexAttributeDescriptions = VertexAttributeDescriptions;
        }
    }
}
