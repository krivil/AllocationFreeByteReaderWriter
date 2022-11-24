namespace AllocationFreeByteReaderWriter;

using System;
using System.Runtime.CompilerServices;

public static partial class ByteReader {
    /// <summary>
    ///     This method copies from one span to the other. It is slow and you shouldn't use it
    /// </summary>
    /// <param name="source">Span from which to read.</param>
    /// <param name="value">Span which should be filled from source span.</param>
    /// <param name="rest">What's left from source after copying.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryRead(this ReadOnlySpan<byte> source, Span<byte> value, out ReadOnlySpan<byte> rest) {
        if (source.Length < value.Length) {
            rest = source;
            return false;
        }

        source[..value.Length].CopyTo(value);
        rest = source[value.Length..];
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryRead(this ReadOnlySpan<byte> source, out ReadOnlySpan<byte> value, int length,
        out ReadOnlySpan<byte> rest) {
        if (source.Length < length) {
            rest = source;
            value = ReadOnlySpan<byte>.Empty;
            return false;
        }

        value = source[..length];
        rest = source[value.Length..];
        return true;
    }
}