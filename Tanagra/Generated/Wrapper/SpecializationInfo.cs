using System;
using System.Runtime.InteropServices;

namespace Vulkan
{
    unsafe public class SpecializationInfo
    {
        internal Interop.SpecializationInfo* NativePointer;
        
        /// <summary>
        /// Array of map entries
        /// </summary>
        public SpecializationMapEntry[] MapEntries
        {
            get
            {
                var valueCount = NativePointer->MapEntryCount;
                var valueArray = new SpecializationMapEntry[valueCount];
                var ptr = (SpecializationMapEntry*)NativePointer->MapEntries;
                for(var x = 0; x < valueCount; x++)
                    valueArray[x] = ptr[x];
                return valueArray;
            }
            set
            {
                var valueCount = value.Length;
                NativePointer->MapEntryCount = (UInt32)valueCount;
                NativePointer->MapEntries = Marshal.AllocHGlobal(Marshal.SizeOf<SpecializationMapEntry>() * valueCount);
                var ptr = (SpecializationMapEntry*)NativePointer->MapEntries;
                for(var x = 0; x < valueCount; x++)
                    ptr[x] = value[x];
            }
        }
        
        /// <summary>
        /// Pointer to SpecConstant data
        /// </summary>
        public IntPtr[] Data
        {
            get
            {
                var valueCount = NativePointer->DataSize;
                var valueArray = new IntPtr[valueCount];
                var ptr = (IntPtr*)NativePointer->Data;
                for(var x = 0; x < valueCount; x++)
                    valueArray[x] = ptr[x];
                return valueArray;
            }
            set
            {
                var valueCount = value.Length;
                NativePointer->DataSize = (UInt32)valueCount;
                NativePointer->Data = Marshal.AllocHGlobal(Marshal.SizeOf<IntPtr>() * valueCount);
                var ptr = (IntPtr*)NativePointer->Data;
                for(var x = 0; x < valueCount; x++)
                    ptr[x] = value[x];
            }
        }
        
        public SpecializationInfo()
        {
            NativePointer = (Interop.SpecializationInfo*)Interop.Structure.Allocate(typeof(Interop.SpecializationInfo));
        }
        
        public SpecializationInfo(SpecializationMapEntry[] MapEntries, IntPtr[] Data) : this()
        {
            this.MapEntries = MapEntries;
            this.Data = Data;
        }
    }
}
