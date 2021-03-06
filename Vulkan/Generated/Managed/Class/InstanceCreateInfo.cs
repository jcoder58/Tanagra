using System;
using System.Runtime.InteropServices;

namespace Vulkan.Managed
{
    unsafe public class InstanceCreateInfo : IDisposable
    {
        internal Unmanaged.InstanceCreateInfo* NativePointer;
        
        /// <summary>
        /// Reserved (Optional)
        /// </summary>
        public InstanceCreateFlags Flags
        {
            get { return NativePointer->Flags; }
            set { NativePointer->Flags = value; }
        }
        
        ApplicationInfo _ApplicationInfo;
        public ApplicationInfo ApplicationInfo
        {
            get { return _ApplicationInfo; }
            set { _ApplicationInfo = value; NativePointer->ApplicationInfo = (IntPtr)value.NativePointer; }
        }
        
        /// <summary>
        /// Ordered list of layer names to be enabled
        /// </summary>
        public String[] EnabledLayerNames
        {
            get
            {
                if(NativePointer->EnabledLayerNames == IntPtr.Zero)
                    return null;
                var valueCount = NativePointer->EnabledLayerCount;
                var valueArray = new String[valueCount];
                var ptr = (void**)NativePointer->EnabledLayerNames;
                for(var x = 0; x < valueCount; x++)
                    valueArray[x] = Marshal.PtrToStringAnsi((IntPtr)ptr[x]);
                
                return valueArray;
            }
            set
            {
                if(value != null)
                {
                    var valueCount = value.Length;
                    var typeSize = Marshal.SizeOf(typeof(IntPtr)) * valueCount;
                    if(NativePointer->EnabledLayerNames != IntPtr.Zero)
                        Marshal.ReAllocHGlobal(NativePointer->EnabledLayerNames, (IntPtr)typeSize);
                    
                    if(NativePointer->EnabledLayerNames == IntPtr.Zero)
                        NativePointer->EnabledLayerNames = Marshal.AllocHGlobal(typeSize);
                    
                    NativePointer->EnabledLayerCount = (UInt32)valueCount;
                    var ptr = (void**)NativePointer->EnabledLayerNames;
                    for(var x = 0; x < valueCount; x++)
                        ptr[x] = (void*)Marshal.StringToHGlobalAnsi(value[x]);
                }
                else
                {
                    if(NativePointer->EnabledLayerNames != IntPtr.Zero)
                        Marshal.FreeHGlobal(NativePointer->EnabledLayerNames);
                    
                    NativePointer->EnabledLayerNames = IntPtr.Zero;
                    NativePointer->EnabledLayerCount = 0;
                }
            }
        }
        
        /// <summary>
        /// Extension names to be enabled
        /// </summary>
        public String[] EnabledExtensionNames
        {
            get
            {
                if(NativePointer->EnabledExtensionNames == IntPtr.Zero)
                    return null;
                var valueCount = NativePointer->EnabledExtensionCount;
                var valueArray = new String[valueCount];
                var ptr = (void**)NativePointer->EnabledExtensionNames;
                for(var x = 0; x < valueCount; x++)
                    valueArray[x] = Marshal.PtrToStringAnsi((IntPtr)ptr[x]);
                
                return valueArray;
            }
            set
            {
                if(value != null)
                {
                    var valueCount = value.Length;
                    var typeSize = Marshal.SizeOf(typeof(IntPtr)) * valueCount;
                    if(NativePointer->EnabledExtensionNames != IntPtr.Zero)
                        Marshal.ReAllocHGlobal(NativePointer->EnabledExtensionNames, (IntPtr)typeSize);
                    
                    if(NativePointer->EnabledExtensionNames == IntPtr.Zero)
                        NativePointer->EnabledExtensionNames = Marshal.AllocHGlobal(typeSize);
                    
                    NativePointer->EnabledExtensionCount = (UInt32)valueCount;
                    var ptr = (void**)NativePointer->EnabledExtensionNames;
                    for(var x = 0; x < valueCount; x++)
                        ptr[x] = (void*)Marshal.StringToHGlobalAnsi(value[x]);
                }
                else
                {
                    if(NativePointer->EnabledExtensionNames != IntPtr.Zero)
                        Marshal.FreeHGlobal(NativePointer->EnabledExtensionNames);
                    
                    NativePointer->EnabledExtensionNames = IntPtr.Zero;
                    NativePointer->EnabledExtensionCount = 0;
                }
            }
        }
        
        public InstanceCreateInfo()
        {
            NativePointer = (Unmanaged.InstanceCreateInfo*)MemUtil.Alloc(typeof(Unmanaged.InstanceCreateInfo));
            NativePointer->SType = StructureType.InstanceCreateInfo;
        }
        
        /// <param name="EnabledLayerNames">Ordered list of layer names to be enabled</param>
        /// <param name="EnabledExtensionNames">Extension names to be enabled</param>
        public InstanceCreateInfo(String[] EnabledLayerNames, String[] EnabledExtensionNames) : this()
        {
            this.EnabledLayerNames = EnabledLayerNames;
            this.EnabledExtensionNames = EnabledExtensionNames;
        }
        
        public void Dispose()
        {
            Marshal.FreeHGlobal(NativePointer->EnabledLayerNames);
            Marshal.FreeHGlobal(NativePointer->EnabledExtensionNames);
            MemUtil.Free((IntPtr)NativePointer);
            NativePointer = null;
            GC.SuppressFinalize(this);
        }
        
        ~InstanceCreateInfo()
        {
            if(NativePointer != null)
            {
                Marshal.FreeHGlobal(NativePointer->EnabledLayerNames);
                Marshal.FreeHGlobal(NativePointer->EnabledExtensionNames);
                MemUtil.Free((IntPtr)NativePointer);
                NativePointer = null;
            }
        }
    }
}
