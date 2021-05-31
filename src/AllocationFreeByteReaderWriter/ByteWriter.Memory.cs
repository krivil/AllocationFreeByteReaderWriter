namespace AllocationFreeByteReaderWriter {
    using System;
    using System.Buffers;
    using System.Runtime.CompilerServices;

    public static partial class ByteWriter {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in ReadOnlySpan<byte> value, out Span<byte> rest) {
            if (destination.Length < value.Length * sizeof(byte)) {
                rest = destination;
                return false;
            }

            value.CopyTo(destination);
            rest = destination[value.Length..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in ReadOnlyMemory<byte> value,
            out Span<byte> rest) =>
            TryWrite(destination, value.Span, out rest);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in ReadOnlySequence<byte> value,
            out Span<byte> rest) {
            if (destination.Length < value.Length) {
                rest = destination;
                return false;
            }

            value.CopyTo(destination);
            rest = destination[(int)value.Length..];
            return true;
        }
    }
}