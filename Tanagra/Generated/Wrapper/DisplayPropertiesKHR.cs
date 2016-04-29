using System;
using System.Runtime.InteropServices;

namespace Vulkan
{
    unsafe public class DisplayPropertiesKHR
    {
        internal Interop.DisplayPropertiesKHR* NativePointer;
        
        DisplayKHR _Display;
        public DisplayKHR Display
        {
            get { return _Display; }
            set { _Display = value; NativePointer->Display = (IntPtr)value.NativePointer; }
        }
        
        public string DisplayName
        {
            get { return Marshal.PtrToStringAnsi(NativePointer->DisplayName); }
            set { NativePointer->DisplayName = Marshal.StringToHGlobalAnsi(value); }
        }
        
        Extent2D _PhysicalDimensions;
        public Extent2D PhysicalDimensions
        {
            get { return _PhysicalDimensions; }
            set { _PhysicalDimensions = value; NativePointer->PhysicalDimensions = (IntPtr)value.NativePointer; }
        }
        
        Extent2D _PhysicalResolution;
        public Extent2D PhysicalResolution
        {
            get { return _PhysicalResolution; }
            set { _PhysicalResolution = value; NativePointer->PhysicalResolution = (IntPtr)value.NativePointer; }
        }
        
        public SurfaceTransformFlags SupportedTransforms
        {
            get { return NativePointer->SupportedTransforms; }
            set { NativePointer->SupportedTransforms = value; }
        }
        
        public Boolean PlaneReorderPossible
        {
            get { return NativePointer->PlaneReorderPossible; }
            set { NativePointer->PlaneReorderPossible = value; }
        }
        
        public Boolean PersistentContent
        {
            get { return NativePointer->PersistentContent; }
            set { NativePointer->PersistentContent = value; }
        }
        
        public DisplayPropertiesKHR()
        {
            NativePointer = (Interop.DisplayPropertiesKHR*)Interop.Structure.Allocate(typeof(Interop.DisplayPropertiesKHR));
        }
    }
}
