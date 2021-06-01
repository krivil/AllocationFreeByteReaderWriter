namespace AllocationFreeByteReaderWriter.Serializable
{
    using System;

    public interface IBinarySerializable {
        int CalculateSizeForBytesSerialization();
        bool ToBytes(Span<byte> bytes, out Span<byte> rest);
    }
}