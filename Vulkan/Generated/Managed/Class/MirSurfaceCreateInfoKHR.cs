using System;
using System.Runtime.InteropServices;

namespace Vulkan.Managed
{
    unsafe public class MirSurfaceCreateInfoKHR : IDisposable
    {
        internal Unmanaged.MirSurfaceCreateInfoKHR* NativePointer;
        
        /// <summary>
        /// Reserved (Optional)
        /// </summary>
        public MirSurfaceCreateFlagsKHR Flags
        {
            get { return NativePointer->Flags; }
            set { NativePointer->Flags = value; }
        }
        
        public IntPtr Connection
        {
            get { return NativePointer->Connection; }
            set { NativePointer->Connection = value; }
        }
        
        public IntPtr MirSurface
        {
            get { return NativePointer->MirSurface; }
            set { NativePointer->MirSurface = value; }
        }
        
        public MirSurfaceCreateInfoKHR()
        {
            NativePointer = (Unmanaged.MirSurfaceCreateInfoKHR*)MemoryUtils.Allocate(typeof(Unmanaged.MirSurfaceCreateInfoKHR));
            NativePointer->SType = StructureType.MirSurfaceCreateInfoKHR;
        }
        
        public MirSurfaceCreateInfoKHR(IntPtr Connection, IntPtr MirSurface) : this()
        {
            this.Connection = Connection;
            this.MirSurface = MirSurface;
        }
        
        public void Dispose()
        {
            MemoryUtils.Free((IntPtr)NativePointer);
            NativePointer = (Unmanaged.MirSurfaceCreateInfoKHR*)IntPtr.Zero;
            GC.SuppressFinalize(this);
        }
        
        ~MirSurfaceCreateInfoKHR()
        {
            if(NativePointer != (Unmanaged.MirSurfaceCreateInfoKHR*)IntPtr.Zero)
            {
                MemoryUtils.Free((IntPtr)NativePointer);
                NativePointer = (Unmanaged.MirSurfaceCreateInfoKHR*)IntPtr.Zero;
            }
        }
    }
}