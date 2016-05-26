using System;
using System.Runtime.InteropServices;

namespace Vulkan.Managed
{
    unsafe public class MappedMemoryRange : IDisposable
    {
        internal Unmanaged.MappedMemoryRange* NativePointer;
        
        DeviceMemory _Memory;
        /// <summary>
        /// Mapped memory object
        /// </summary>
        public DeviceMemory Memory
        {
            get { return _Memory; }
            set { _Memory = value; NativePointer->Memory = value.NativePointer; }
        }
        
        /// <summary>
        /// Offset within the memory object where the range starts
        /// </summary>
        public DeviceSize Offset
        {
            get { return NativePointer->Offset; }
            set { NativePointer->Offset = value; }
        }
        
        /// <summary>
        /// Size of the range within the memory object
        /// </summary>
        public DeviceSize Size
        {
            get { return NativePointer->Size; }
            set { NativePointer->Size = value; }
        }
        
        public MappedMemoryRange()
        {
            NativePointer = (Unmanaged.MappedMemoryRange*)MemoryUtils.Allocate(typeof(Unmanaged.MappedMemoryRange));
            NativePointer->SType = StructureType.MappedMemoryRange;
        }
        
        public MappedMemoryRange(DeviceMemory Memory, DeviceSize Offset, DeviceSize Size) : this()
        {
            this.Memory = Memory;
            this.Offset = Offset;
            this.Size = Size;
        }
        
        public void Dispose()
        {
            MemoryUtils.Free((IntPtr)NativePointer);
            NativePointer = null;
            GC.SuppressFinalize(this);
        }
        
        ~MappedMemoryRange()
        {
            if(NativePointer != null)
            {
                MemoryUtils.Free((IntPtr)NativePointer);
                NativePointer = null;
            }
        }
    }
}
