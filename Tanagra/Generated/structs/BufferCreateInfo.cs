using System;
using System.Runtime.InteropServices;

namespace Vulkan
{
    [StructLayout(LayoutKind.Sequential)]
    public struct BufferCreateInfo
    {
        public StructureType sType;
        public IntPtr pNext;
        public BufferCreateFlags flags;
        public DeviceSize size;
        public BufferUsageFlags usage;
        public SharingMode sharingMode;
        public UInt32 queueFamilyIndexCount;
        public UInt32 pQueueFamilyIndices;
    }
}
