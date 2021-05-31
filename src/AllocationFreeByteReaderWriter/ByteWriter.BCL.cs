namespace AllocationFreeByteReaderWriter {
    using System;
    using System.Buffers;
    using System.Runtime.CompilerServices;
    using System.Text;

    public static partial class ByteWriter {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in string value, out Span<byte> rest)
            => TryWrite(destination, value, out rest, Encoding.UTF8);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in string value, out Span<byte> rest,
            Encoding encoding) {
            int encodedStringLength = encoding.GetByteCount(value);

            if (destination.Length < encodedStringLength + IntLengthInBytes) {
                rest = destination;
                return false;
            }

            rest = destination;

            byte[] byteBuffer = ArrayPool<byte>.Shared.Rent(encodedStringLength);
            Span<byte> byteSpan = byteBuffer[..encodedStringLength];

            try {
                _ = encoding.GetBytes(value, byteSpan);
                return TryWrite(destination, encodedStringLength, out rest) &&
                       TryWrite(rest, byteSpan, out rest);
            } finally {
                ArrayPool<byte>.Shared.Return(byteBuffer);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in ReadOnlySpan<char> value, out Span<byte> rest)
            => TryWrite(destination, value, out rest, Encoding.UTF8);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in ReadOnlySpan<char> value, out Span<byte> rest,
            Encoding encoding) {
            int encodedStringLength = encoding.GetByteCount(value);

            if (destination.Length < encodedStringLength + IntLengthInBytes) {
                rest = destination;
                return false;
            }

            rest = destination;

            byte[] byteBuffer = ArrayPool<byte>.Shared.Rent(encodedStringLength);
            Span<byte> byteSpan = byteBuffer[..encodedStringLength];

            try {
                _ = encoding.GetBytes(value, byteSpan);
                return TryWrite(destination, encodedStringLength, out rest) &&
                       TryWrite(rest, byteSpan, out rest);
            } finally {
                ArrayPool<byte>.Shared.Return(byteBuffer);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in DateTime value, out Span<byte> rest)
            => TryWrite(destination, value.ToBinary(), out rest);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in TimeSpan value, out Span<byte> rest)
            => TryWrite(destination, value.Ticks, out rest);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in Guid value, out Span<byte> rest) {
            if (destination.Length < 16) {
                rest = destination;
                return false;
            }

            bool result = value.TryWriteBytes(destination);
            rest = destination[16..];
            return result;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in DateTimeOffset value, out Span<byte> rest) {
            if (destination.Length < LongLengthInBytes + ShortLengthInBytes) {
                rest = destination;
                return false;
            }

            bool result = destination.TryWrite(value.Date, out rest) &&
            rest.TryWrite((short)(value.Offset.Ticks / TimeSpan.TicksPerMinute), out rest);
            
            return result;
        }
    }
}