namespace AllocationFreeByteReaderWriter.Persistable;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

public static class IPersistableExtensions {

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryWrite<T>(this Span<byte> span, T value, out Span<byte> rest)
        where T : IPersistable<T> => value.TryWrite(span, out rest);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryRead<T>(this ReadOnlySpan<byte> source, [MaybeNullWhen(false)] out T value, out ReadOnlySpan<byte> rest) 
        where T : IPersistable<T> => T.TryRead(source, out value, out rest);
}