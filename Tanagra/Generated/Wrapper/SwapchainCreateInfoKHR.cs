using System;
using System.Runtime.InteropServices;

namespace Vulkan
{
    unsafe public class SwapchainCreateInfoKHR
    {
        internal Interop.SwapchainCreateInfoKHR* NativePointer;
        
        /// <summary>
        /// Reserved
        /// </summary>
        public SwapchainCreateFlagsKHR Flags
        {
            get { return NativePointer->Flags; }
            set { NativePointer->Flags = value; }
        }
        
        SurfaceKHR _Surface;
        /// <summary>
        /// The swapchain's target surface
        /// </summary>
        public SurfaceKHR Surface
        {
            get { return _Surface; }
            set { _Surface = value; NativePointer->Surface = value.NativePointer; }
        }
        
        /// <summary>
        /// Minimum number of presentation images the application needs
        /// </summary>
        public UInt32 MinImageCount
        {
            get { return NativePointer->MinImageCount; }
            set { NativePointer->MinImageCount = value; }
        }
        
        /// <summary>
        /// Format of the presentation images
        /// </summary>
        public Format ImageFormat
        {
            get { return NativePointer->ImageFormat; }
            set { NativePointer->ImageFormat = value; }
        }
        
        /// <summary>
        /// Colorspace of the presentation images
        /// </summary>
        public ColorSpaceKHR ImageColorSpace
        {
            get { return NativePointer->ImageColorSpace; }
            set { NativePointer->ImageColorSpace = value; }
        }
        
        /// <summary>
        /// Dimensions of the presentation images
        /// </summary>
        public Extent2D ImageExtent
        {
            get { return NativePointer->ImageExtent; }
            set { NativePointer->ImageExtent = value; }
        }
        
        /// <summary>
        /// Determines the number of views for multiview/stereo presentation
        /// </summary>
        public UInt32 ImageArrayLayers
        {
            get { return NativePointer->ImageArrayLayers; }
            set { NativePointer->ImageArrayLayers = value; }
        }
        
        /// <summary>
        /// Bits indicating how the presentation images will be used
        /// </summary>
        public ImageUsageFlags ImageUsage
        {
            get { return NativePointer->ImageUsage; }
            set { NativePointer->ImageUsage = value; }
        }
        
        /// <summary>
        /// Sharing mode used for the presentation images
        /// </summary>
        public SharingMode ImageSharingMode
        {
            get { return NativePointer->ImageSharingMode; }
            set { NativePointer->ImageSharingMode = value; }
        }
        
        /// <summary>
        /// Array of queue family indices having access to the images in case of concurrent sharing mode
        /// </summary>
        public UInt32[] QueueFamilyIndices
        {
            get
            {
                var valueCount = NativePointer->QueueFamilyIndexCount;
                var valueArray = new UInt32[valueCount];
                var ptr = (UInt32*)NativePointer->QueueFamilyIndices;
                for(var x = 0; x < valueCount; x++)
                    valueArray[x] = ptr[x];
                return valueArray;
            }
            set
            {
                var valueCount = value.Length;
                NativePointer->QueueFamilyIndexCount = (UInt32)valueCount;
                NativePointer->QueueFamilyIndices = Marshal.AllocHGlobal(Marshal.SizeOf<UInt32>() * valueCount);
                var ptr = (UInt32*)NativePointer->QueueFamilyIndices;
                for(var x = 0; x < valueCount; x++)
                    ptr[x] = value[x];
            }
        }
        
        /// <summary>
        /// The transform, relative to the device's natural orientation, applied to the image content prior to presentation
        /// </summary>
        public SurfaceTransformFlagsKHR PreTransform
        {
            get { return NativePointer->PreTransform; }
            set { NativePointer->PreTransform = value; }
        }
        
        /// <summary>
        /// The alpha blending mode used when compositing this surface with other surfaces in the window system
        /// </summary>
        public CompositeAlphaFlagsKHR CompositeAlpha
        {
            get { return NativePointer->CompositeAlpha; }
            set { NativePointer->CompositeAlpha = value; }
        }
        
        /// <summary>
        /// Which presentation mode to use for presents on this swap chain
        /// </summary>
        public PresentModeKHR PresentMode
        {
            get { return NativePointer->PresentMode; }
            set { NativePointer->PresentMode = value; }
        }
        
        /// <summary>
        /// Specifies whether presentable images may be affected by window clip regions
        /// </summary>
        public Bool32 Clipped
        {
            get { return NativePointer->Clipped; }
            set { NativePointer->Clipped = value; }
        }
        
        SwapchainKHR _OldSwapchain;
        /// <summary>
        /// Existing swap chain to replace, if any
        /// </summary>
        public SwapchainKHR OldSwapchain
        {
            get { return _OldSwapchain; }
            set { _OldSwapchain = value; NativePointer->OldSwapchain = value.NativePointer; }
        }
        
        public SwapchainCreateInfoKHR()
        {
            NativePointer = (Interop.SwapchainCreateInfoKHR*)Interop.Structure.Allocate(typeof(Interop.SwapchainCreateInfoKHR));
            NativePointer->SType = StructureType.SwapchainCreateInfoKHR;
        }
        
        public SwapchainCreateInfoKHR(SurfaceKHR Surface, UInt32 MinImageCount, Format ImageFormat, ColorSpaceKHR ImageColorSpace, Extent2D ImageExtent, UInt32 ImageArrayLayers, ImageUsageFlags ImageUsage, SharingMode ImageSharingMode, UInt32[] QueueFamilyIndices, SurfaceTransformFlagsKHR PreTransform, CompositeAlphaFlagsKHR CompositeAlpha, PresentModeKHR PresentMode, Bool32 Clipped) : this()
        {
            this.Surface = Surface;
            this.MinImageCount = MinImageCount;
            this.ImageFormat = ImageFormat;
            this.ImageColorSpace = ImageColorSpace;
            this.ImageExtent = ImageExtent;
            this.ImageArrayLayers = ImageArrayLayers;
            this.ImageUsage = ImageUsage;
            this.ImageSharingMode = ImageSharingMode;
            this.QueueFamilyIndices = QueueFamilyIndices;
            this.PreTransform = PreTransform;
            this.CompositeAlpha = CompositeAlpha;
            this.PresentMode = PresentMode;
            this.Clipped = Clipped;
        }
    }
}
