namespace AllocationFreeByteReaderWriter.Tests
{
    using System;
    using System.Text;
    using Xunit;

    public class BclTests {
        [Fact]
        public void String() {
            string expected = "Test123";

            byte[] buffer = Array.Empty<byte>();
            Span<byte> span = buffer;

            bool success = span.TryWrite(expected, out Span<byte> rest, Encoding.UTF8);
            Assert.False(success);

            success = ((ReadOnlySpan<byte>)buffer).TryRead(out string actual, out ReadOnlySpan<byte> rest2, Encoding.UTF8);
            Assert.False(success);

            buffer = new byte[sizeof(int)];
            span = buffer;

            success = span.TryWrite(expected, out rest, Encoding.UTF8);
            Assert.False(success);

            success = ((ReadOnlySpan<byte>)buffer).TryRead(out actual, out rest2, Encoding.UTF8);
            Assert.True(success);
            Assert.Equal(string.Empty, actual);

            buffer = new byte[sizeof(int) + Encoding.UTF8.GetByteCount(expected)];
            span = buffer;

            success = span.TryWrite(expected, out rest, Encoding.UTF8);
            Assert.True(success);
            Assert.True(rest.IsEmpty);

            success = ((ReadOnlySpan<byte>)buffer).TryRead(out actual, out rest2, Encoding.UTF8);
            Assert.True(success);
            Assert.True(rest2.IsEmpty);

            Assert.Equal(expected, actual);
        }
    }
}