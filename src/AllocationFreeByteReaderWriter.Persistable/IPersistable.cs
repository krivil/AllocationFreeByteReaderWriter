namespace AllocationFreeByteReaderWriter.Persistable;

public interface IPersistable<T> where T : IPersistable<T> {
    abstract static bool TryRead(ReadOnlySpan<byte> from, out T? value, out ReadOnlySpan<byte> rest);

    int GetPersistedSizeInBytes();
    bool TryWrite(Span<byte> to, out Span<byte> rest);
}
