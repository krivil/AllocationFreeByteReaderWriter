namespace AllocationFreeByteReaderWriter;

using System;
using System.Runtime.CompilerServices;
using System.Text;

public static partial class ByteReader {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryRead(this ReadOnlySpan<byte> source, out ReadOnlySpan<char> value, out ReadOnlySpan<byte> rest)
        => TryRead(source, out value, out rest, Encoding.UTF8);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryRead(this ReadOnlySpan<byte> source, out ReadOnlySpan<char> value, out ReadOnlySpan<byte> rest,
        Encoding encoding) {
        if (TryRead(source, out string s, out rest, encoding)) {

            value = s;
            return true;
        }

        value = ReadOnlySpan<char>.Empty;
        rest = source;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryRead(this ReadOnlySpan<byte> source, out string value, out ReadOnlySpan<byte> rest)
        => TryRead(source, out value, out rest, Encoding.UTF8);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryRead(this ReadOnlySpan<byte> source, out string value, out ReadOnlySpan<byte> rest,
        Encoding encoding) {
        bool success = TryRead(source, out int length, out rest);
        if (!success || rest.Length < length) {
            value = string.Empty;
            return false;
        }

        value = encoding.GetString(rest[..length]);
        rest = rest[value.Length..];
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryRead(this ReadOnlySpan<byte> source, out DateTime value, out ReadOnlySpan<byte> rest) {
        if (TryRead(source, out long ticks, out rest)) {
            value = DateTime.FromBinary(ticks);
            return true;
        }

        value = default;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryRead(this ReadOnlySpan<byte> source, out TimeSpan value, out ReadOnlySpan<byte> rest) {
        if (TryRead(source, out long ticks, out rest)) {
            value = TimeSpan.FromTicks(ticks);
            return true;
        }

        value = default;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryRead(this ReadOnlySpan<byte> source, out Guid value, out ReadOnlySpan<byte> rest) {
        if (TryRead(source, out ReadOnlySpan<byte> bytes, 16, out rest)) {
            value = new Guid(bytes);
            return true;
        }

        rest = source;
        value = Guid.Empty;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryRead(this ReadOnlySpan<byte> source, out DateTimeOffset value, out ReadOnlySpan<byte> rest) {
        if (source.Length < LongLengthInBytes + ShortLengthInBytes) {
            value = default;
            rest = source;
            return false;
        }

        if (TryRead(source, out long ticks, out rest) && TryRead(rest, out short offset, out rest)) {
            value = new DateTimeOffset(new DateTime(ticks, DateTimeKind.Unspecified), TimeSpan.FromMinutes(offset));
            return true;
        }

        value = default;
        return false;
    }
}