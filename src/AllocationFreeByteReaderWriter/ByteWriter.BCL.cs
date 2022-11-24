namespace AllocationFreeByteReaderWriter;

using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Text;

public static partial class ByteWriter {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int GetSize(in string value) => GetSize(value, Encoding.UTF8);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int GetSize(in string value, Encoding encoding) => IntLengthInBytes + encoding.GetByteCount(value);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int GetSize(in ReadOnlySpan<char> value) => GetSize(value, Encoding.UTF8);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int GetSize(in ReadOnlySpan<char> value, Encoding encoding) => IntLengthInBytes + encoding.GetByteCount(value);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int GetSize(in DateTime value) => LongLengthInBytes;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int GetSize(in TimeSpan value) => LongLengthInBytes;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int GetSize(in Guid value) => 16;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int GetSize(in DateTimeOffset value) => LongLengthInBytes + ShortLengthInBytes;


    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool TryWrite(this Span<byte> destination, string value, out Span<byte> rest)
        => TryWrite(destination, value, out rest, Encoding.UTF8);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool TryWrite(this Span<byte> destination, string value, out Span<byte> rest,
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
                   TryWriteRaw(rest, byteSpan, out rest);
        } finally {
            ArrayPool<byte>.Shared.Return(byteBuffer);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool TryWrite(this Span<byte> destination, ReadOnlySpan<char> value, out Span<byte> rest)
        => TryWrite(destination, value, out rest, Encoding.UTF8);
        
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool TryWrite(this Span<byte> destination, ReadOnlySpan<char> value, out Span<byte> rest,
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
                   TryWriteRaw(rest, byteSpan, out rest);
        } finally {
            ArrayPool<byte>.Shared.Return(byteBuffer);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool TryWrite(this Span<byte> destination, DateTime value, out Span<byte> rest)
        => TryWrite(destination, value.ToBinary(), out rest);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool TryWrite(this Span<byte> destination, TimeSpan value, out Span<byte> rest)
        => TryWrite(destination, value.Ticks, out rest);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool TryWrite(this Span<byte> destination, in Guid value, out Span<byte> rest) {
        if (destination.Length < 16) {
            rest = destination;
            return false;
        }

        bool result = value.TryWriteBytes(destination);
        rest = destination[16..];
        return result;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool TryWrite(this Span<byte> destination, DateTimeOffset value, out Span<byte> rest) {
        if (destination.Length < LongLengthInBytes + ShortLengthInBytes) {
            rest = destination;
            return false;
        }

        bool result = destination.TryWrite(value.Date, out rest) &&
                      rest.TryWrite((short)(value.Offset.Ticks / TimeSpan.TicksPerMinute), out rest);
            
        return result;
    }
}