namespace AllocationFreeByteReaderWriter.Tests;

using AllocationFreeByteReaderWriter.Persistable;
using System;
using System.Text;
using Xunit;

public class PersistableTests {
    [Fact]
    public void PersistUnpersist() {
        var expected = new TestPersistable {
            IntegerProperty = 1,
            StringProperty  = "asdf",
        };

        byte[] buffer = Array.Empty<byte>();
        Span<byte> span = buffer;

        bool success = span.TryWrite(expected, out _);
        Assert.False(success);

        ReadOnlySpan<byte> roSpan = span;
        success = roSpan.TryRead<TestPersistable>(out TestPersistable? actual, out _);
        Assert.False(success);

        buffer = new byte[expected.GetPersistedSizeInBytes()];
        span = buffer;

        success = span.TryWrite(expected, out Span<byte> rest);
        Assert.True(success);
        Assert.True(rest.IsEmpty);

        success = ((ReadOnlySpan<byte>)span).TryRead<TestPersistable>(out actual, out ReadOnlySpan<byte> rest2);
        Assert.True(success);
        Assert.True(rest2.IsEmpty);

        Assert.Equal(expected, actual);
    }

    public sealed record TestPersistable : IPersistable<TestPersistable> {
        public static bool TryRead(ReadOnlySpan<byte> from, out TestPersistable value, out ReadOnlySpan<byte> rest)
            => InitializeOuts(out value, out rest)
            && from.TryRead(out int integerProp, out rest)
            && rest.TryRead(out string stringProp, out rest, Encoding.UTF8)
            && CreateInstance(integerProp, stringProp, out value);

        public required int IntegerProperty { get; init; }
        
        public required string StringProperty { get; init; }

        public int GetPersistedSizeInBytes() => sizeof(int) + sizeof(int) + Encoding.UTF8.GetByteCount(StringProperty);
        
        public bool TryWrite(Span<byte> to, out Span<byte> rest)
            => to.TryWrite(IntegerProperty, out rest)
            && rest.TryWrite(StringProperty, out rest, Encoding.UTF8);

        private static bool InitializeOuts(out TestPersistable persistable, out ReadOnlySpan<byte> rest) {
            persistable = default!;
            rest = default;
            return true;
        }

        private static bool CreateInstance(int integerProperty, string stringProperty, out TestPersistable persistable) {
            persistable = new TestPersistable() {
                IntegerProperty = integerProperty,
                StringProperty = stringProperty,
            };
            return true;
        }
    }
}
