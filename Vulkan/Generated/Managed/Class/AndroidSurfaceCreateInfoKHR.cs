using System;
using System.Runtime.InteropServices;

namespace Vulkan.Managed
{
    unsafe public class AndroidSurfaceCreateInfoKHR : IDisposable
    {
        internal Unmanaged.AndroidSurfaceCreateInfoKHR* NativePointer;
        
        /// <summary>
        /// Reserved (Optional)
        /// </summary>
        public AndroidSurfaceCreateFlagsKHR Flags
        {
            get { return NativePointer->Flags; }
            set { NativePointer->Flags = value; }
        }
        
        public IntPtr Window
        {
            get { return NativePointer->Window; }
            set { NativePointer->Window = value; }
        }
        
        public AndroidSurfaceCreateInfoKHR()
        {
            NativePointer = (Unmanaged.AndroidSurfaceCreateInfoKHR*)MemUtil.Alloc(typeof(Unmanaged.AndroidSurfaceCreateInfoKHR));
            NativePointer->SType = StructureType.AndroidSurfaceCreateInfoKHR;
        }
        
        public AndroidSurfaceCreateInfoKHR(IntPtr Window) : this()
        {
            this.Window = Window;
        }
        
        public void Dispose()
        {
            MemUtil.Free((IntPtr)NativePointer);
            NativePointer = null;
            GC.SuppressFinalize(this);
        }
        
        ~AndroidSurfaceCreateInfoKHR()
        {
            if(NativePointer != null)
            {
                MemUtil.Free((IntPtr)NativePointer);
                NativePointer = null;
            }
        }
    }
}
