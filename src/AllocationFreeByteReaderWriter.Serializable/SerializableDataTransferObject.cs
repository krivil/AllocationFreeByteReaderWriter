namespace AllocationFreeByteReaderWriter.Serializable
{
    using System;

    public abstract record SerializableDataTransferObject : IBinarySerializable {
        protected SerializableDataTransferObject() {
        }

        public SerializableDataTransferObject(ReadOnlySpan<byte> bytes, out ReadOnlySpan<byte> rest) {
            rest = ReadOnlySpan<byte>.Empty;
        }

        public abstract int CalculateSizeForBytesSerialization();
        public abstract bool ToBytes(Span<byte> bytes, out Span<byte> rest);
    }
}