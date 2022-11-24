namespace AllocationFreeByteReaderWriter;

using System;
using System.Runtime.CompilerServices;

public static partial class ByteWriter {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetSize(in byte[] value) => IntLengthInBytes + value.Length;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryWrite(this Span<byte> destination, byte[] value, out Span<byte> rest)
        => TryWrite(destination, value.Length, out rest) 
           && TryWriteRaw(rest, new ReadOnlySpan<byte>(value), out rest);
}