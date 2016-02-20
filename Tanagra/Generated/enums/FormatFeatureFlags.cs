using System;

namespace Vulkan
{
    [Flags]
    public enum FormatFeatureFlags
    {
        FORMAT_FEATURE_SAMPLED_IMAGE_BIT = 0,
        FORMAT_FEATURE_STORAGE_IMAGE_BIT = 1,
        FORMAT_FEATURE_STORAGE_IMAGE_ATOMIC_BIT = 2,
        FORMAT_FEATURE_UNIFORM_TEXEL_BUFFER_BIT = 3,
        FORMAT_FEATURE_STORAGE_TEXEL_BUFFER_BIT = 4,
        FORMAT_FEATURE_STORAGE_TEXEL_BUFFER_ATOMIC_BIT = 5,
        FORMAT_FEATURE_VERTEX_BUFFER_BIT = 6,
        FORMAT_FEATURE_COLOR_ATTACHMENT_BIT = 7,
        FORMAT_FEATURE_COLOR_ATTACHMENT_BLEND_BIT = 8,
        FORMAT_FEATURE_DEPTH_STENCIL_ATTACHMENT_BIT = 9,
        FORMAT_FEATURE_BLIT_SRC_BIT = 10,
        FORMAT_FEATURE_BLIT_DST_BIT = 11,
        FORMAT_FEATURE_SAMPLED_IMAGE_FILTER_LINEAR_BIT = 12,
    }
}
