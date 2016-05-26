using System;
using System.Runtime.InteropServices;

namespace Vulkan.Managed
{
    unsafe public class SparseMemoryBind : IDisposable
    {
        internal Unmanaged.SparseMemoryBind* NativePointer;
        
        /// <summary>
        /// Specified in bytes
        /// </summary>
        public DeviceSize ResourceOffset
        {
            get { return NativePointer->ResourceOffset; }
            set { NativePointer->ResourceOffset = value; }
        }
        
        /// <summary>
        /// Specified in bytes
        /// </summary>
        public DeviceSize Size
        {
            get { return NativePointer->Size; }
            set { NativePointer->Size = value; }
        }
        
        DeviceMemory _Memory;
        public DeviceMemory Memory
        {
            get { return _Memory; }
            set { _Memory = value; NativePointer->Memory = value.NativePointer; }
        }
        
        /// <summary>
        /// Specified in bytes
        /// </summary>
        public DeviceSize MemoryOffset
        {
            get { return NativePointer->MemoryOffset; }
            set { NativePointer->MemoryOffset = value; }
        }
        
        /// <summary>
        /// Reserved for future (Optional)
        /// </summary>
        public SparseMemoryBindFlags Flags
        {
            get { return NativePointer->Flags; }
            set { NativePointer->Flags = value; }
        }
        
        public SparseMemoryBind()
        {
            NativePointer = (Unmanaged.SparseMemoryBind*)MemUtil.Alloc(typeof(Unmanaged.SparseMemoryBind));
        }
        
        public SparseMemoryBind(DeviceSize ResourceOffset, DeviceSize Size, DeviceSize MemoryOffset) : this()
        {
            this.ResourceOffset = ResourceOffset;
            this.Size = Size;
            this.MemoryOffset = MemoryOffset;
        }
        
        public void Dispose()
        {
            MemUtil.Free((IntPtr)NativePointer);
            NativePointer = null;
            GC.SuppressFinalize(this);
        }
        
        ~SparseMemoryBind()
        {
            if(NativePointer != null)
            {
                MemUtil.Free((IntPtr)NativePointer);
                NativePointer = null;
            }
        }
    }
}
