using System;

namespace Vulkan
{
    [Flags]
    public enum BufferUsageFlags
    {
        BUFFER_USAGE_TRANSFER_SRC_BIT = 0,
        BUFFER_USAGE_TRANSFER_DST_BIT = 1,
        BUFFER_USAGE_UNIFORM_TEXEL_BUFFER_BIT = 2,
        BUFFER_USAGE_STORAGE_TEXEL_BUFFER_BIT = 3,
        BUFFER_USAGE_UNIFORM_BUFFER_BIT = 4,
        BUFFER_USAGE_STORAGE_BUFFER_BIT = 5,
        BUFFER_USAGE_INDEX_BUFFER_BIT = 6,
        BUFFER_USAGE_VERTEX_BUFFER_BIT = 7,
        BUFFER_USAGE_INDIRECT_BUFFER_BIT = 8,
    }
}
