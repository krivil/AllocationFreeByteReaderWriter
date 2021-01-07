namespace AllocationFreeByteReaderWriter.Tests {
    using System;
    using System.Text;
    using Xunit;

    public class UnitTests {
        [Fact]
        public void TestBool() {
            bool expected = true;
            byte[] buffer = new byte[sizeof(bool)];
            Span<byte> span = buffer;

            bool success = span.TryWrite(expected, out Span<byte> rest);
            Assert.True(success);
            Assert.True(rest.IsEmpty);

            success = ((ReadOnlySpan<byte>)span).TryRead(out bool actual, out ReadOnlySpan<byte> rest2);
            Assert.True(success);
            Assert.True(rest2.IsEmpty);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestByte() {
            byte expected = byte.MaxValue;
            byte[] buffer = new byte[sizeof(byte)];
            Span<byte> span = buffer;

            bool success = span.TryWrite(expected, out Span<byte> rest);
            Assert.True(success);
            Assert.True(rest.IsEmpty);

            success = ((ReadOnlySpan<byte>)span).TryRead(out byte actual, out ReadOnlySpan<byte> rest2);
            Assert.True(success);
            Assert.True(rest2.IsEmpty);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestSByte() {
            sbyte expected = sbyte.MaxValue;
            byte[] buffer = new byte[sizeof(sbyte)];
            Span<byte> span = buffer;

            bool success = span.TryWrite(expected, out Span<byte> rest);
            Assert.True(success);
            Assert.True(rest.IsEmpty);

            success = ((ReadOnlySpan<byte>)span).TryRead(out sbyte actual, out ReadOnlySpan<byte> rest2);
            Assert.True(success);
            Assert.True(rest2.IsEmpty);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestShort() {
            short expected = short.MaxValue;
            byte[] buffer = new byte[sizeof(short)];
            Span<byte> span = buffer;

            bool success = span.TryWrite(expected, out Span<byte> rest);
            Assert.True(success);
            Assert.True(rest.IsEmpty);

            success = ((ReadOnlySpan<byte>)span).TryRead(out short actual, out ReadOnlySpan<byte> rest2);
            Assert.True(success);
            Assert.True(rest2.IsEmpty);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Testfloat() {
            float expected = float.MaxValue;
            byte[] buffer = new byte[sizeof(float)];
            Span<byte> span = buffer;

            bool success = span.TryWrite(expected, out Span<byte> rest);
            Assert.True(success);
            Assert.True(rest.IsEmpty);

            success = ((ReadOnlySpan<byte>)span).TryRead(out float actual, out ReadOnlySpan<byte> rest2);
            Assert.True(success);
            Assert.True(rest2.IsEmpty);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestInt() {
            int expected = int.MaxValue;
            byte[] buffer = new byte[sizeof(int)];
            Span<byte> span = buffer;

            bool success = span.TryWrite(expected, out Span<byte> rest);
            Assert.True(success);
            Assert.True(rest.IsEmpty);

            success = ((ReadOnlySpan<byte>)span).TryRead(out int actual, out ReadOnlySpan<byte> rest2);
            Assert.True(success);
            Assert.True(rest2.IsEmpty);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestUInt() {
            uint expected = uint.MaxValue;
            byte[] buffer = new byte[sizeof(uint)];
            Span<byte> span = buffer;

            bool success = span.TryWrite(expected, out Span<byte> rest);
            Assert.True(success);
            Assert.True(rest.IsEmpty);

            success = ((ReadOnlySpan<byte>)span).TryRead(out uint actual, out ReadOnlySpan<byte> rest2);
            Assert.True(success);
            Assert.True(rest2.IsEmpty);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestLong() {
            long expected = long.MaxValue;
            byte[] buffer = new byte[sizeof(long)];
            Span<byte> span = buffer;

            bool success = span.TryWrite(expected, out Span<byte> rest);
            Assert.True(success);
            Assert.True(rest.IsEmpty);

            success = ((ReadOnlySpan<byte>)span).TryRead(out long actual, out ReadOnlySpan<byte> rest2);
            Assert.True(success);
            Assert.True(rest2.IsEmpty);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestULong() {
            ulong expected = ulong.MaxValue;
            byte[] buffer = new byte[sizeof(ulong)];
            Span<byte> span = buffer;

            bool success = span.TryWrite(expected, out Span<byte> rest);
            Assert.True(success);
            Assert.True(rest.IsEmpty);

            success = ((ReadOnlySpan<byte>)span).TryRead(out ulong actual, out ReadOnlySpan<byte> rest2);
            Assert.True(success);
            Assert.True(rest2.IsEmpty);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestChar() {
            char expected = char.MaxValue;
            byte[] buffer = new byte[sizeof(char)];
            Span<byte> span = buffer;

            bool success = span.TryWrite(expected, out Span<byte> rest);
            Assert.True(success);
            Assert.True(rest.IsEmpty);

            success = ((ReadOnlySpan<byte>)span).TryRead(out char actual, out ReadOnlySpan<byte> rest2);
            Assert.True(success);
            Assert.True(rest2.IsEmpty);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestFloat() {
            float expected = float.MaxValue;
            byte[] buffer = new byte[sizeof(float)];
            Span<byte> span = buffer;

            bool success = span.TryWrite(expected, out Span<byte> rest);
            Assert.True(success);
            Assert.True(rest.IsEmpty);

            success = ((ReadOnlySpan<byte>)span).TryRead(out float actual, out ReadOnlySpan<byte> rest2);
            Assert.True(success);
            Assert.True(rest2.IsEmpty);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestDouble() {
            double expected = double.MaxValue;
            byte[] buffer = new byte[sizeof(double)];
            Span<byte> span = buffer;

            bool success = span.TryWrite(expected, out Span<byte> rest);
            Assert.True(success);
            Assert.True(rest.IsEmpty);

            success = ((ReadOnlySpan<byte>)span).TryRead(out double actual, out ReadOnlySpan<byte> rest2);
            Assert.True(success);
            Assert.True(rest2.IsEmpty);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestDecimal() {
            decimal expected = decimal.MaxValue;
            byte[] buffer = new byte[sizeof(decimal)];
            Span<byte> span = buffer;

            bool success = span.TryWrite(expected, out Span<byte> rest);
            Assert.True(success);
            Assert.True(rest.IsEmpty);

            success = ((ReadOnlySpan<byte>)span).TryRead(out decimal actual, out ReadOnlySpan<byte> rest2);
            Assert.True(success);
            Assert.True(rest2.IsEmpty);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestString() {
            string expected = "Test123";
            byte[] buffer = new byte[sizeof(int) + Encoding.UTF8.GetByteCount(expected)];
            Span<byte> span = buffer;

            bool success = span.TryWrite(expected, out Span<byte> rest, Encoding.UTF8);
            Assert.True(success);
            Assert.True(rest.IsEmpty);

            success = ((ReadOnlySpan<byte>)buffer).TryRead(out string actual, out ReadOnlySpan<byte> rest2, Encoding.UTF8);
            Assert.True(success);
            Assert.True(rest2.IsEmpty);

            Assert.Equal(expected, actual);
        }
    }
}