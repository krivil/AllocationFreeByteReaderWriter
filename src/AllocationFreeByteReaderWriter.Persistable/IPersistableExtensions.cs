namespace AllocationFreeByteReaderWriter.Persistable;

using System.Runtime.CompilerServices;

public static class IPersistableExtensions {

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryWrite<T>(this in Span<byte> span, T value, out Span<byte> rest)
        where T : IPersistable<T> => value.TryWrite(span, out rest);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryRead<T>(this in ReadOnlySpan<byte> source, out T? value, out ReadOnlySpan<byte> rest) 
        where T : IPersistable<T> => T.TryRead(source, out value, out rest);
}