using System;
using System.Runtime.InteropServices;

namespace Vulkan.Managed
{
    unsafe public class PipelineRasterizationStateRasterizationOrderAMD : IDisposable
    {
        internal Unmanaged.PipelineRasterizationStateRasterizationOrderAMD* NativePointer;
        
        /// <summary>
        /// Rasterization order to use for the pipeline
        /// </summary>
        public RasterizationOrderAMD RasterizationOrder
        {
            get { return NativePointer->RasterizationOrder; }
            set { NativePointer->RasterizationOrder = value; }
        }
        
        public PipelineRasterizationStateRasterizationOrderAMD()
        {
            NativePointer = (Unmanaged.PipelineRasterizationStateRasterizationOrderAMD*)MemoryUtils.Allocate(typeof(Unmanaged.PipelineRasterizationStateRasterizationOrderAMD));
            NativePointer->SType = StructureType.PipelineRasterizationStateRasterizationOrderAMD;
        }
        
        public PipelineRasterizationStateRasterizationOrderAMD(RasterizationOrderAMD RasterizationOrder) : this()
        {
            this.RasterizationOrder = RasterizationOrder;
        }
        
        public void Dispose()
        {
            MemoryUtils.Free((IntPtr)NativePointer);
            NativePointer = null;
            GC.SuppressFinalize(this);
        }
        
        ~PipelineRasterizationStateRasterizationOrderAMD()
        {
            if(NativePointer != null)
            {
                MemoryUtils.Free((IntPtr)NativePointer);
                NativePointer = null;
            }
        }
    }
}
