using System;
using System.Runtime.InteropServices;

namespace Vulkan.Managed
{
    unsafe public class DisplayPropertiesKHR : IDisposable
    {
        internal Unmanaged.DisplayPropertiesKHR* NativePointer;
        
        DisplayKHR _Display;
        /// <summary>
        /// Handle of the display object
        /// </summary>
        public DisplayKHR Display
        {
            get { return _Display; }
            set { _Display = value; NativePointer->Display = value.NativePointer; }
        }
        
        /// <summary>
        /// Name of the display
        /// </summary>
        public String DisplayName
        {
            get { return Marshal.PtrToStringAnsi(NativePointer->DisplayName); }
            set { NativePointer->DisplayName = Marshal.StringToHGlobalAnsi(value); }
        }
        
        /// <summary>
        /// In millimeters?
        /// </summary>
        public Extent2D PhysicalDimensions
        {
            get { return NativePointer->PhysicalDimensions; }
            set { NativePointer->PhysicalDimensions = value; }
        }
        
        /// <summary>
        /// Max resolution for CRT?
        /// </summary>
        public Extent2D PhysicalResolution
        {
            get { return NativePointer->PhysicalResolution; }
            set { NativePointer->PhysicalResolution = value; }
        }
        
        /// <summary>
        /// One or more bits from VkSurfaceTransformFlagsKHR (Optional)
        /// </summary>
        public SurfaceTransformFlagsKHR SupportedTransforms
        {
            get { return NativePointer->SupportedTransforms; }
            set { NativePointer->SupportedTransforms = value; }
        }
        
        /// <summary>
        /// VK_TRUE if the overlay plane's z-order can be changed on this display.
        /// </summary>
        public Bool32 PlaneReorderPossible
        {
            get { return NativePointer->PlaneReorderPossible; }
            set { NativePointer->PlaneReorderPossible = value; }
        }
        
        /// <summary>
        /// VK_TRUE if this is a "smart" display that supports self-refresh/internal buffering.
        /// </summary>
        public Bool32 PersistentContent
        {
            get { return NativePointer->PersistentContent; }
            set { NativePointer->PersistentContent = value; }
        }
        
        public DisplayPropertiesKHR()
        {
            NativePointer = (Unmanaged.DisplayPropertiesKHR*)MemUtil.Alloc(typeof(Unmanaged.DisplayPropertiesKHR));
        }
        
        /// <param name="Display">Handle of the display object</param>
        /// <param name="DisplayName">Name of the display</param>
        /// <param name="PhysicalDimensions">In millimeters?</param>
        /// <param name="PhysicalResolution">Max resolution for CRT?</param>
        /// <param name="PlaneReorderPossible">VK_TRUE if the overlay plane's z-order can be changed on this display.</param>
        /// <param name="PersistentContent">VK_TRUE if this is a "smart" display that supports self-refresh/internal buffering.</param>
        public DisplayPropertiesKHR(DisplayKHR Display, String DisplayName, Extent2D PhysicalDimensions, Extent2D PhysicalResolution, Bool32 PlaneReorderPossible, Bool32 PersistentContent) : this()
        {
            this.Display = Display;
            this.DisplayName = DisplayName;
            this.PhysicalDimensions = PhysicalDimensions;
            this.PhysicalResolution = PhysicalResolution;
            this.PlaneReorderPossible = PlaneReorderPossible;
            this.PersistentContent = PersistentContent;
        }
        
        public void Dispose()
        {
            Marshal.FreeHGlobal(NativePointer->DisplayName);
            MemUtil.Free((IntPtr)NativePointer);
            NativePointer = null;
            GC.SuppressFinalize(this);
        }
        
        ~DisplayPropertiesKHR()
        {
            if(NativePointer != null)
            {
                Marshal.FreeHGlobal(NativePointer->DisplayName);
                MemUtil.Free((IntPtr)NativePointer);
                NativePointer = null;
            }
        }
    }
}
