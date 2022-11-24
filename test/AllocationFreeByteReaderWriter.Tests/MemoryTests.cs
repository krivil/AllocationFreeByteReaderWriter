namespace AllocationFreeByteReaderWriter.Tests;

using System;
using System.Text;
using Xunit;

public class MemoryTests {
    [Fact]
    public void Span() {
        ReadOnlySpan<byte> expected = Encoding.ASCII.GetBytes("Test123");

        byte[] buffer = Array.Empty<byte>();
        Span<byte> span = buffer;

        bool success = span.TryWriteRaw(expected, out Span<byte> rest);
        Assert.False(success);

        success = ((ReadOnlySpan<byte>)buffer).TryRead(out ReadOnlySpan<byte> actual, expected.Length, out ReadOnlySpan<byte> rest2);
        Assert.False(success);

        buffer = new byte[expected.Length];
        span = buffer;

        success = span.TryWriteRaw(expected, out rest);
        Assert.True(success);
        Assert.True(rest.IsEmpty);

        success = ((ReadOnlySpan<byte>)buffer).TryRead(out actual, expected.Length, out rest2);
        Assert.True(success);
        Assert.True(rest2.IsEmpty);

        Assert.Equal(expected.ToArray(), actual.ToArray());
    }
}
