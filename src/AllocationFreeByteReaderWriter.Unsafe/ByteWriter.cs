namespace AllocationFreeByteReaderWriter {
    using System;
    using System.Runtime.CompilerServices;

    public static partial class ByteWriter {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe bool TryWrite<T>(this in Span<byte> destination, in ReadOnlySpan<T> value, out Span<byte> rest)
            where T: unmanaged {
            int valueLength = value.Length * sizeof(T);
            if (destination.Length < valueLength) {
                rest = destination;
                return false;
            }

            fixed (void* valueRef = value) {
                fixed (void* destinationRef = destination) {
                    Buffer.MemoryCopy(valueRef, destinationRef,
                        valueLength, valueLength);
                }
            }
            
            //value.CopyTo(destination);
            rest = destination[valueLength..];
            return true;
        }
        
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // public static unsafe bool TryWrite<T>(this in Span<byte> destination, in ReadOnlyMemory<T> value, out Span<byte> rest)
        //     where T: unmanaged {
        //     int valueLength = value.Length * sizeof(T);
        //     if (destination.Length < valueLength) {
        //         rest = destination;
        //         return false;
        //     }
        //
        //     fixed (void* valueRef = value.Span) {
        //         fixed (void* destinationRef = destination) {
        //             Buffer.MemoryCopy(valueRef, destinationRef,
        //                 valueLength, valueLength);
        //         }
        //     }
        //     
        //     //value.CopyTo(destination);
        //     rest = destination[valueLength..];
        //     return true;
        // }
    }
}