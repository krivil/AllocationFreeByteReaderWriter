namespace AllocationFreeByteReaderWriter {
    using System;

    public static partial class ByteWriter {
        public static bool TryWrite(this in Span<byte> destination, in byte[] value, out Span<byte> rest)
            => TryWrite(in destination, new ReadOnlySpan<byte>(value), out rest);
    }
}