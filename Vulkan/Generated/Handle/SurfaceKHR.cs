using System;
using System.Runtime.InteropServices;

namespace Vulkan
{
    /// <summary>
    /// Vulkan handle. Nondispatchable. Child of <see cref="Instance"/>.
    /// </summary>
    public class SurfaceKHR
    {
        internal UInt64 NativePointer;
        
        public override string ToString() => "SurfaceKHR 0x" + NativePointer.ToString("X8");
    }
}
