namespace AllocationFreeByteReaderWriter.Tests;

using Serializable;
using System;
using Xunit;

public class DtoTests {
    [Fact]
    public void Initialization() => DataTransferObjectSerializer.RegisterOrReplaceType<TimestampDto>(1);

    [Fact]
    public void SerializationAndDeserialization() {
        DataTransferObjectSerializer.RegisterOrReplaceType<TimestampDto>(1);

        var dto = new TimestampDto(DateTime.UtcNow, 1);

        int len = dto.CalculateSizeForBytesSerialization();

        byte[] buffer = new byte[len + 5];

        Span<byte> span = buffer.AsSpan();

        dto.ToBytes(span, out Span<byte> rest);

        Assert.Equal(5, rest.Length);

        bool success = DataTransferObjectSerializer.TryDeserialize(1, span, out SerializableDataTransferObject? dtoValue, out ReadOnlySpan<byte> rest2);

        Assert.True(success);

        var dto2 = dtoValue as TimestampDto;

        Assert.Equal(5, rest2.Length);

        Assert.Equal(dto, dto2);
    }
}

public record TimestampDto : SerializableDataTransferObject {
    public static bool TryFromBytes(ReadOnlySpan<byte> bytes, out TimestampDto value, out ReadOnlySpan<byte> rest) {
        ulong sequenceNumber = 0;
        bool success = bytes.TryRead(out long timestampUtc, out rest)
                           && rest.TryRead(out sequenceNumber, out rest);

        value = new TimestampDto(DateTime.FromBinary(timestampUtc), sequenceNumber);

        return success;
    }

    public DateTime TimestampUtc { get; }
    public ulong SequenceNumber { get; }

    public TimestampDto(DateTime timestampUtc, ulong sequenceNumber) {
        TimestampUtc = timestampUtc;
        SequenceNumber = sequenceNumber;
    }

    public TimestampDto(ReadOnlySpan<byte> bytes, out ReadOnlySpan<byte> rest)
        : base(bytes, out _) {
        if (!bytes.TryRead(out long timestampUtc, out rest)) throw new DeserializeException(typeof(TimestampDto));
        if (!rest.TryRead(out ulong sequenceNumber, out rest)) throw new DeserializeException(typeof(TimestampDto));

        TimestampUtc = DateTime.FromBinary(timestampUtc);
        SequenceNumber = sequenceNumber;
    }

    public override int CalculateSizeForBytesSerialization() =>
        ByteWriter.LongLengthInBytes + ByteWriter.ULongLengthInBytes;

    public override bool ToBytes(Span<byte> bytes, out Span<byte> rest) =>
        bytes.TryWrite(TimestampUtc.ToBinary(), out rest)
        && rest.TryWrite(SequenceNumber, out rest);
}
