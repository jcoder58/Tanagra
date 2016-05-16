using System;
using System.Runtime.InteropServices;

namespace Vulkan
{
    unsafe public class PipelineRasterizationStateCreateInfo
    {
        internal Interop.PipelineRasterizationStateCreateInfo* NativePointer;
        
        /// <summary>
        /// Reserved
        /// </summary>
        public PipelineRasterizationStateCreateFlags Flags
        {
            get { return NativePointer->Flags; }
            set { NativePointer->Flags = value; }
        }
        
        public Bool32 DepthClampEnable
        {
            get { return NativePointer->DepthClampEnable; }
            set { NativePointer->DepthClampEnable = value; }
        }
        
        public Bool32 RasterizerDiscardEnable
        {
            get { return NativePointer->RasterizerDiscardEnable; }
            set { NativePointer->RasterizerDiscardEnable = value; }
        }
        
        /// <summary>
        /// Optional (GL45)
        /// </summary>
        public PolygonMode PolygonMode
        {
            get { return NativePointer->PolygonMode; }
            set { NativePointer->PolygonMode = value; }
        }
        
        public CullModeFlags CullMode
        {
            get { return NativePointer->CullMode; }
            set { NativePointer->CullMode = value; }
        }
        
        public FrontFace FrontFace
        {
            get { return NativePointer->FrontFace; }
            set { NativePointer->FrontFace = value; }
        }
        
        public Bool32 DepthBiasEnable
        {
            get { return NativePointer->DepthBiasEnable; }
            set { NativePointer->DepthBiasEnable = value; }
        }
        
        public Single DepthBiasConstantFactor
        {
            get { return NativePointer->DepthBiasConstantFactor; }
            set { NativePointer->DepthBiasConstantFactor = value; }
        }
        
        public Single DepthBiasClamp
        {
            get { return NativePointer->DepthBiasClamp; }
            set { NativePointer->DepthBiasClamp = value; }
        }
        
        public Single DepthBiasSlopeFactor
        {
            get { return NativePointer->DepthBiasSlopeFactor; }
            set { NativePointer->DepthBiasSlopeFactor = value; }
        }
        
        public Single LineWidth
        {
            get { return NativePointer->LineWidth; }
            set { NativePointer->LineWidth = value; }
        }
        
        public PipelineRasterizationStateCreateInfo()
        {
            NativePointer = (Interop.PipelineRasterizationStateCreateInfo*)Interop.Structure.Allocate(typeof(Interop.PipelineRasterizationStateCreateInfo));
            NativePointer->SType = StructureType.PipelineRasterizationStateCreateInfo;
        }
        
        public PipelineRasterizationStateCreateInfo(Bool32 DepthClampEnable, Bool32 RasterizerDiscardEnable, PolygonMode PolygonMode, FrontFace FrontFace, Bool32 DepthBiasEnable, Single DepthBiasConstantFactor, Single DepthBiasClamp, Single DepthBiasSlopeFactor, Single LineWidth) : this()
        {
            this.DepthClampEnable = DepthClampEnable;
            this.RasterizerDiscardEnable = RasterizerDiscardEnable;
            this.PolygonMode = PolygonMode;
            this.FrontFace = FrontFace;
            this.DepthBiasEnable = DepthBiasEnable;
            this.DepthBiasConstantFactor = DepthBiasConstantFactor;
            this.DepthBiasClamp = DepthBiasClamp;
            this.DepthBiasSlopeFactor = DepthBiasSlopeFactor;
            this.LineWidth = LineWidth;
        }
    }
}
