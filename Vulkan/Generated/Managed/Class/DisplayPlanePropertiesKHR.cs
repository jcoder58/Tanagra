using System;
using System.Runtime.InteropServices;

namespace Vulkan.Managed
{
    unsafe public class DisplayPlanePropertiesKHR : IDisposable
    {
        internal Unmanaged.DisplayPlanePropertiesKHR* NativePointer;
        
        DisplayKHR _CurrentDisplay;
        /// <summary>
        /// Display the plane is currently associated with. Will be VK_NULL_HANDLE if the plane is not in use.
        /// </summary>
        public DisplayKHR CurrentDisplay
        {
            get { return _CurrentDisplay; }
            set { _CurrentDisplay = value; NativePointer->CurrentDisplay = value.NativePointer; }
        }
        
        /// <summary>
        /// Current z-order of the plane.
        /// </summary>
        public UInt32 CurrentStackIndex
        {
            get { return NativePointer->CurrentStackIndex; }
            set { NativePointer->CurrentStackIndex = value; }
        }
        
        public DisplayPlanePropertiesKHR()
        {
            NativePointer = (Unmanaged.DisplayPlanePropertiesKHR*)MemUtil.Alloc(typeof(Unmanaged.DisplayPlanePropertiesKHR));
        }
        
        /// <param name="CurrentDisplay">Display the plane is currently associated with. Will be VK_NULL_HANDLE if the plane is not in use.</param>
        /// <param name="CurrentStackIndex">Current z-order of the plane.</param>
        public DisplayPlanePropertiesKHR(DisplayKHR CurrentDisplay, UInt32 CurrentStackIndex) : this()
        {
            this.CurrentDisplay = CurrentDisplay;
            this.CurrentStackIndex = CurrentStackIndex;
        }
        
        public void Dispose()
        {
            MemUtil.Free((IntPtr)NativePointer);
            NativePointer = null;
            GC.SuppressFinalize(this);
        }
        
        ~DisplayPlanePropertiesKHR()
        {
            if(NativePointer != null)
            {
                MemUtil.Free((IntPtr)NativePointer);
                NativePointer = null;
            }
        }
    }
}
