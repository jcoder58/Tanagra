using System;
using System.Runtime.InteropServices;

namespace Vulkan
{
    /// <summary>
    /// Vulkan handle. Dispatchable. Child of <see cref="CommandPool"/>.
    /// </summary>
    public class CommandBuffer
    {
        internal IntPtr NativePointer;
        
        public override string ToString() => "CommandBuffer 0x" + NativePointer.ToString("X8");
    }
}
