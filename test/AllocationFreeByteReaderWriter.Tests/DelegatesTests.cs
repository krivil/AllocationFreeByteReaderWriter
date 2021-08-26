namespace AllocationFreeByteReaderWriter.Tests;

using Serializable;
using System;
using Xunit;

public class DelegatesTests {
    [Fact]
    public void TestInitialization() {
        DelegateSerializer.RegisterOrReplaceType(1, (_) => sizeof(int),
            (int value, Span<byte> bytes, out Span<byte> rest) => bytes.TryWrite(value, out rest),
            (ReadOnlySpan<byte> bytes, out int value, out ReadOnlySpan<byte> rest) => bytes.TryRead(out value, out rest));
        DelegateSerializer.RegisterOrReplaceType(2, (_) => sizeof(long),
            (long value, Span<byte> bytes, out Span<byte> rest) => bytes.TryWrite(value, out rest),
            (ReadOnlySpan<byte> bytes, out long value, out ReadOnlySpan<byte> rest) => bytes.TryRead(out value, out rest));
        DelegateSerializer.RegisterOrReplaceType(3, (_) => sizeof(double),
            (double value, Span<byte> bytes, out Span<byte> rest) => bytes.TryWrite(value, out rest),
            (ReadOnlySpan<byte> bytes, out double value, out ReadOnlySpan<byte> rest) => bytes.TryRead(out value, out rest));
        DelegateSerializer.RegisterOrReplaceType(4, (_) => sizeof(decimal),
            (decimal value, Span<byte> bytes, out Span<byte> rest) => bytes.TryWrite(value, out rest),
            (ReadOnlySpan<byte> bytes, out decimal value, out ReadOnlySpan<byte> rest) => bytes.TryRead(out value, out rest));
    }

    [Fact]
    public void TestSerializationAndDeserialization() {
        DelegateSerializer.RegisterOrReplaceType(1, (_) => sizeof(int),
            (int value, Span<byte> bytes, out Span<byte> rest) => bytes.TryWrite(value, out rest),
            (ReadOnlySpan<byte> bytes, out int value, out ReadOnlySpan<byte> rest) => bytes.TryRead(out value, out rest));
        DelegateSerializer.RegisterOrReplaceType(2, (_) => sizeof(long),
            (long value, Span<byte> bytes, out Span<byte> rest) => bytes.TryWrite(value, out rest),
            (ReadOnlySpan<byte> bytes, out long value, out ReadOnlySpan<byte> rest) => bytes.TryRead(out value, out rest));
        DelegateSerializer.RegisterOrReplaceType(3, (_) => sizeof(double),
            (double value, Span<byte> bytes, out Span<byte> rest) => bytes.TryWrite(value, out rest),
            (ReadOnlySpan<byte> bytes, out double value, out ReadOnlySpan<byte> rest) => bytes.TryRead(out value, out rest));
        DelegateSerializer.RegisterOrReplaceType(4, (_) => sizeof(decimal),
            (decimal value, Span<byte> bytes, out Span<byte> rest) => bytes.TryWrite(value, out rest),
            (ReadOnlySpan<byte> bytes, out decimal value, out ReadOnlySpan<byte> rest) => bytes.TryRead(out value, out rest));

        DelegateSerializer.RegisterOrReplaceType(5, value => value.CalculateSizeForBytesSerialization(),
            (TimestampDto value, Span<byte> bytes, out Span<byte> rest) => value.ToBytes(bytes, out rest),
            (ReadOnlySpan<byte> bytes, out TimestampDto value, out ReadOnlySpan<byte> rest) => TimestampDto.TryFromBytes(bytes, out value, out rest));

        //

        byte[] buffer = new byte[16];

        Span<byte> span = buffer.AsSpan();

        int intValue = 7;

        bool success = DelegateSerializer.TrySerialize(1, intValue, span, out Span<byte> rest);

        Assert.True(success);
        Assert.Equal(16 - sizeof(int), rest.Length);

        success = DelegateSerializer.TryDeserialize(1, span, out int intValue2, out ReadOnlySpan<byte> rest2);

        Assert.True(success);
        Assert.Equal(16 - sizeof(int), rest2.Length);

        Assert.Equal(intValue, intValue2);

        //

        buffer = new byte[16];

        span = buffer.AsSpan();

        long longValue = 7;

        success = DelegateSerializer.TrySerialize(2, longValue, span, out rest);

        Assert.True(success);
        Assert.Equal(16 - sizeof(long), rest.Length);

        success = DelegateSerializer.TryDeserialize(2, span, out long longValue2, out rest2);

        Assert.True(success);
        Assert.Equal(16 - sizeof(long), rest2.Length);

        Assert.Equal(longValue, longValue2);

        //

        buffer = new byte[16];

        span = buffer.AsSpan();

        double doubleValue = 7.0;

        success = DelegateSerializer.TrySerialize(3, doubleValue, span, out rest);

        Assert.True(success);
        Assert.Equal(16 - sizeof(double), rest.Length);

        success = DelegateSerializer.TryDeserialize(3, span, out double doubleValue2, out rest2);

        Assert.True(success);
        Assert.Equal(16 - sizeof(double), rest2.Length);

        Assert.Equal(doubleValue, doubleValue2);

        //

        buffer = new byte[16];

        span = buffer.AsSpan();

        decimal decimalValue = 7.0m;

        success = DelegateSerializer.TrySerialize(4, decimalValue, span, out rest);

        Assert.True(success);
        Assert.Equal(16 - sizeof(decimal), rest.Length);

        success = DelegateSerializer.TryDeserialize(4, span, out decimal decimalValue2, out rest2);

        Assert.True(success);
        Assert.Equal(16 - sizeof(decimal), rest2.Length);

        Assert.Equal(decimalValue, decimalValue2);

        //

        var dto = new TimestampDto(DateTime.UtcNow, 1);

        int len = DelegateSerializer.GetSizeInBytesForSerialization(5, dto);

        Assert.NotEqual(-1, len);

        buffer = new byte[len + 5];

        span = buffer.AsSpan();

        success = DelegateSerializer.TrySerialize(5, dto, span, out rest);

        Assert.True(success);

        Assert.Equal(5, rest.Length);

        success = DelegateSerializer.TryDeserialize(5, span, out TimestampDto dtoValue, out rest2);

        Assert.True(success);

        var dto2 = dtoValue as TimestampDto;

        Assert.Equal(5, rest2.Length);

        Assert.Equal(dto, dto2);
    }
}
