namespace AllocationFreeByteReaderWriter.Persistable;

using System;
using System.Diagnostics.CodeAnalysis;

public interface IPersistable<T> where T : IPersistable<T> {
    abstract static bool TryRead(ReadOnlySpan<byte> from, [MaybeNullWhen(false)] out T value, out ReadOnlySpan<byte> rest);

    int GetPersistedSizeInBytes();
    bool TryWrite(Span<byte> to, out Span<byte> rest);
}
