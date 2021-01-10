namespace AllocationFreeByteReaderWriter.Tests {
    using System;
    using System.Text;
    using Xunit;

    public class UnitTests {
        [Fact]
        public void TestBool() {
            bool expected = true;
            byte[] buffer = Array.Empty<byte>();
            Span<byte> span = buffer;

            bool success = span.TryWrite(expected, out Span<byte> rest);
            Assert.False(success);

            success = ((ReadOnlySpan<byte>)span).TryRead(out bool actual, out ReadOnlySpan<byte> rest2);
            Assert.False(success);

            buffer = new byte[sizeof(bool)];
            span = buffer;

            success = span.TryWrite(expected, out rest);
            Assert.True(success);
            Assert.True(rest.IsEmpty);

            success = ((ReadOnlySpan<byte>)span).TryRead(out actual, out rest2);
            Assert.True(success);
            Assert.True(rest2.IsEmpty);

            Assert.Equal(expected, actual);

            expected = false;
            success = span.TryWrite(expected, out rest);
            Assert.True(success);
            Assert.True(rest.IsEmpty);

            success = ((ReadOnlySpan<byte>)span).TryRead(out actual, out rest2);
            Assert.True(success);
            Assert.True(rest2.IsEmpty);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestByte() {
            const byte expected = byte.MaxValue;
            byte[] buffer = Array.Empty<byte>();
            Span<byte> span = buffer;

            bool success = span.TryWrite(expected, out Span<byte> rest);
            Assert.False(success);

            success = ((ReadOnlySpan<byte>)span).TryRead(out byte actual, out ReadOnlySpan<byte> rest2);
            Assert.False(success);

            buffer = new byte[sizeof(byte)];
            span = buffer;

            success = span.TryWrite(expected, out rest);
            Assert.True(success);
            Assert.True(rest.IsEmpty);

            success = ((ReadOnlySpan<byte>)span).TryRead(out actual, out rest2);
            Assert.True(success);
            Assert.True(rest2.IsEmpty);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestSByte() {
            const sbyte expected = sbyte.MaxValue;
            byte[] buffer = Array.Empty<byte>();
            Span<byte> span = buffer;

            bool success = span.TryWrite(expected, out Span<byte> rest);
            Assert.False(success);

            success = ((ReadOnlySpan<byte>)span).TryRead(out sbyte actual, out ReadOnlySpan<byte> rest2);
            Assert.False(success);

            buffer = new byte[sizeof(sbyte)];
            span = buffer;

            success = span.TryWrite(expected, out rest);
            Assert.True(success);
            Assert.True(rest.IsEmpty);

            success = ((ReadOnlySpan<byte>)span).TryRead(out actual, out rest2);
            Assert.True(success);
            Assert.True(rest2.IsEmpty);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestShort() {
            const short expected = short.MaxValue;
            byte[] buffer = Array.Empty<byte>();
            Span<byte> span = buffer;

            bool success = span.TryWrite(expected, out Span<byte> rest);
            Assert.False(success);

            success = ((ReadOnlySpan<byte>)span).TryRead(out short actual, out ReadOnlySpan<byte> rest2);
            Assert.False(success);

            buffer = new byte[sizeof(short)];
            span = buffer;

            success = span.TryWrite(expected, out rest);
            Assert.True(success);
            Assert.True(rest.IsEmpty);

            success = ((ReadOnlySpan<byte>)span).TryRead(out actual, out rest2);
            Assert.True(success);
            Assert.True(rest2.IsEmpty);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestUShort() {
            const ushort expected = ushort.MaxValue;
            byte[] buffer = Array.Empty<byte>();
            Span<byte> span = buffer;

            bool success = span.TryWrite(expected, out Span<byte> rest);
            Assert.False(success);

            success = ((ReadOnlySpan<byte>)span).TryRead(out ushort actual, out ReadOnlySpan<byte> rest2);
            Assert.False(success);

            buffer = new byte[sizeof(ushort)];
            span = buffer;

            success = span.TryWrite(expected, out rest);
            Assert.True(success);
            Assert.True(rest.IsEmpty);

            success = ((ReadOnlySpan<byte>)span).TryRead(out actual, out rest2);
            Assert.True(success);
            Assert.True(rest2.IsEmpty);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestInt() {
            const int expected = int.MaxValue;
            byte[] buffer = Array.Empty<byte>();
            Span<byte> span = buffer;

            bool success = span.TryWrite(expected, out Span<byte> rest);
            Assert.False(success);

            success = ((ReadOnlySpan<byte>)span).TryRead(out int actual, out ReadOnlySpan<byte> rest2);
            Assert.False(success);

            buffer = new byte[sizeof(int)];
            span = buffer;

            success = span.TryWrite(expected, out rest);
            Assert.True(success);
            Assert.True(rest.IsEmpty);

            success = ((ReadOnlySpan<byte>)span).TryRead(out actual, out rest2);
            Assert.True(success);
            Assert.True(rest2.IsEmpty);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestUInt() {
            const uint expected = uint.MaxValue;
            byte[] buffer = Array.Empty<byte>();
            Span<byte> span = buffer;

            bool success = span.TryWrite(expected, out Span<byte> rest);
            Assert.False(success);

            success = ((ReadOnlySpan<byte>)span).TryRead(out uint actual, out ReadOnlySpan<byte> rest2);
            Assert.False(success);

            buffer = new byte[sizeof(uint)];
            span = buffer;

            success = span.TryWrite(expected, out rest);
            Assert.True(success);
            Assert.True(rest.IsEmpty);

            success = ((ReadOnlySpan<byte>)span).TryRead(out actual, out rest2);
            Assert.True(success);
            Assert.True(rest2.IsEmpty);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestLong() {
            const long expected = long.MaxValue;
            byte[] buffer = Array.Empty<byte>();
            Span<byte> span = buffer;

            bool success = span.TryWrite(expected, out Span<byte> rest);
            Assert.False(success);

            success = ((ReadOnlySpan<byte>)span).TryRead(out long actual, out ReadOnlySpan<byte> rest2);
            Assert.False(success);

            buffer = new byte[sizeof(long)];
            span = buffer;

            success = span.TryWrite(expected, out rest);
            Assert.True(success);
            Assert.True(rest.IsEmpty);

            success = ((ReadOnlySpan<byte>)span).TryRead(out actual, out rest2);
            Assert.True(success);
            Assert.True(rest2.IsEmpty);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestULong() {
            const ulong expected = ulong.MaxValue;
            byte[] buffer = Array.Empty<byte>();
            Span<byte> span = buffer;

            bool success = span.TryWrite(expected, out Span<byte> rest);
            Assert.False(success);

            success = ((ReadOnlySpan<byte>)span).TryRead(out ulong actual, out ReadOnlySpan<byte> rest2);
            Assert.False(success);

            buffer = new byte[sizeof(ulong)];
            span = buffer;

            success = span.TryWrite(expected, out rest);
            Assert.True(success);
            Assert.True(rest.IsEmpty);

            success = ((ReadOnlySpan<byte>)span).TryRead(out actual, out rest2);
            Assert.True(success);
            Assert.True(rest2.IsEmpty);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestChar() {
            const char expected = char.MaxValue;
            byte[] buffer = Array.Empty<byte>();
            Span<byte> span = buffer;

            bool success = span.TryWrite(expected, out Span<byte> rest);
            Assert.False(success);

            success = ((ReadOnlySpan<byte>)span).TryRead(out char actual, out ReadOnlySpan<byte> rest2);
            Assert.False(success);

            buffer = new byte[sizeof(char)];
            span = buffer;

            success = span.TryWrite(expected, out rest);
            Assert.True(success);
            Assert.True(rest.IsEmpty);

            success = ((ReadOnlySpan<byte>)span).TryRead(out actual, out rest2);
            Assert.True(success);
            Assert.True(rest2.IsEmpty);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestFloat() {
            const float expected = float.MaxValue;
            byte[] buffer = Array.Empty<byte>();
            Span<byte> span = buffer;

            bool success = span.TryWrite(expected, out Span<byte> rest);
            Assert.False(success);

            success = ((ReadOnlySpan<byte>)span).TryRead(out float actual, out ReadOnlySpan<byte> rest2);
            Assert.False(success);

            buffer = new byte[sizeof(float)];
            span = buffer;

            success = span.TryWrite(expected, out rest);
            Assert.True(success);
            Assert.True(rest.IsEmpty);

            success = ((ReadOnlySpan<byte>)span).TryRead(out actual, out rest2);
            Assert.True(success);
            Assert.True(rest2.IsEmpty);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestDouble() {
            const double expected = double.MaxValue;
            byte[] buffer = Array.Empty<byte>();
            Span<byte> span = buffer;

            bool success = span.TryWrite(expected, out Span<byte> rest);
            Assert.False(success);

            success = ((ReadOnlySpan<byte>)span).TryRead(out double actual, out ReadOnlySpan<byte> rest2);
            Assert.False(success);

            buffer = new byte[sizeof(double)];
            span = buffer;

            success = span.TryWrite(expected, out rest);
            Assert.True(success);
            Assert.True(rest.IsEmpty);

            success = ((ReadOnlySpan<byte>)span).TryRead(out actual, out rest2);
            Assert.True(success);
            Assert.True(rest2.IsEmpty);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestDecimal() {
            const decimal expected = decimal.MaxValue;
            byte[] buffer = Array.Empty<byte>();
            Span<byte> span = buffer;

            bool success = span.TryWrite(expected, out Span<byte> rest);
            Assert.False(success);

            success = ((ReadOnlySpan<byte>)span).TryRead(out decimal actual, out ReadOnlySpan<byte> rest2);
            Assert.False(success);

            buffer = new byte[sizeof(decimal)];
            span = buffer;

            success = span.TryWrite(expected, out rest);
            Assert.True(success);
            Assert.True(rest.IsEmpty);

            success = ((ReadOnlySpan<byte>)span).TryRead(out actual, out rest2);
            Assert.True(success);
            Assert.True(rest2.IsEmpty);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestString() {
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

        [Fact]
        public void TestSpan() {
            ReadOnlySpan<byte> expected = Encoding.ASCII.GetBytes("Test123");

            byte[] buffer = Array.Empty<byte>();
            Span<byte> span = buffer;

            bool success = span.TryWrite(expected, out Span<byte> rest);
            Assert.False(success);

            success = ((ReadOnlySpan<byte>)buffer).TryRead(out ReadOnlySpan<byte> actual, expected.Length, out ReadOnlySpan<byte> rest2);
            Assert.False(success);

            buffer = new byte[expected.Length];
            span = buffer;

            success = span.TryWrite(expected, out rest);
            Assert.True(success);
            Assert.True(rest.IsEmpty);

            success = ((ReadOnlySpan<byte>)buffer).TryRead(out actual, expected.Length, out rest2);
            Assert.True(success);
            Assert.True(rest2.IsEmpty);

            Assert.Equal(expected.ToArray(), actual.ToArray());
        }
    }
}