namespace AllocationFreeByteReaderWriter.Tests {
    using System;
    using Xunit;

    public class NumericTests {
        [Fact]
        public void Bool() {
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
        public void Byte() {
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
        public void SByte() {
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
        public void Short() {
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
        public void UShort() {
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
        public void Int() {
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
        public void UInt() {
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
        public void Long() {
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
        public void ULong() {
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
        public void Char() {
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
        public void Float() {
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
        public void Double() {
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
        public void Decimal() {
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
    }
}