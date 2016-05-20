using System;
using System.Runtime.InteropServices;

namespace Vulkan.Managed
{
    unsafe public class FenceCreateInfo : IDisposable
    {
        internal Unmanaged.FenceCreateInfo* NativePointer;
        
        /// <summary>
        /// Fence creation flags (Optional)
        /// </summary>
        public FenceCreateFlags Flags
        {
            get { return NativePointer->Flags; }
            set { NativePointer->Flags = value; }
        }
        
        public FenceCreateInfo()
        {
            NativePointer = (Unmanaged.FenceCreateInfo*)MemoryUtils.Allocate(typeof(Unmanaged.FenceCreateInfo));
            NativePointer->SType = StructureType.FenceCreateInfo;
        }
        
        public void Dispose()
        {
            MemoryUtils.Free((IntPtr)NativePointer);
            NativePointer = (Unmanaged.FenceCreateInfo*)IntPtr.Zero;
            GC.SuppressFinalize(this);
        }
        
        ~FenceCreateInfo()
        {
            if(NativePointer != (Unmanaged.FenceCreateInfo*)IntPtr.Zero)
            {
                MemoryUtils.Free((IntPtr)NativePointer);
                NativePointer = (Unmanaged.FenceCreateInfo*)IntPtr.Zero;
            }
        }
    }
}