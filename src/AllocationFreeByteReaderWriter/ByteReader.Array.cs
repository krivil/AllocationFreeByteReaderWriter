namespace AllocationFreeByteReaderWriter;

using System;

public static partial class ByteReader {
    /// <summary>
    ///     This method copies from one span to byte array. It is slow and you shouldn't use it
    /// </summary>
    /// <param name="source">Span from which to read.</param>
    /// <param name="value">Array which should be filled from source span.</param>
    /// <param name="rest">What's left from source after copying.</param>
    /// <returns></returns>
    public static bool TryRead(this ReadOnlySpan<byte> source, out byte[] value, out ReadOnlySpan<byte> rest) {
        bool success = TryRead(source, out int length, out rest);
        if (!success || rest.Length < length) {
            value = Array.Empty<byte>();
            return false;
        }

        value = new byte[length];
        if (!TryRead(rest, out ReadOnlySpan<byte> result, length, out rest)) return false;
        result.CopyTo(value);
        return true;
    }
}