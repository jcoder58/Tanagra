using System;
using System.Runtime.InteropServices;

namespace Vulkan
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MemoryAllocateInfo
    {
        public StructureType sType;
        public IntPtr pNext;
        public DeviceSize allocationSize;
        public UInt32 memoryTypeIndex;
    }
}
