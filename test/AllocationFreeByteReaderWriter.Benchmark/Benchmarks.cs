using BenchmarkDotNet.Attributes;
using AllocationFreeByteReaderWriter;
using BenchmarkDotNet.Configs;
using AllocationFreeByteReaderWriter.Persistable;
using System.Text;

[MemoryDiagnoser]
[ThreadingDiagnoser]
[CsvMeasurementsExporter]
[RPlotExporter]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class Benchmarks {
    private static readonly byte[] _buffer = new byte[1024];
    private static readonly DateTime _date = DateTime.Now;
    private static readonly DateTimeOffset _dateOffset = DateTimeOffset.Now;
    private static readonly TestPersistable _persistable = new(){IntegerProperty = 123, StringProperty = "foo"};

    private byte[] ByteBuffer = Array.Empty<byte>();
    private byte[] Int32Buffer = Array.Empty<byte>();
    private byte[] Int64Buffer = Array.Empty<byte>();
    private byte[] SingleBuffer = Array.Empty<byte>();
    private byte[] DoubleBuffer = Array.Empty<byte>();
    private byte[] StringBuffer = Array.Empty<byte>();
    private byte[] DateTimeBuffer = Array.Empty<byte>();
    private byte[] DateTimeOffsetBuffer = Array.Empty<byte>();
    private byte[] PersistableBuffer = Array.Empty<byte>();

    [GlobalSetup]
    public void GlobalSetup() {
        ByteBuffer = new byte[1];
        ByteBuffer.AsSpan().TryWrite((byte)123, out _);

        Int32Buffer = new byte[sizeof(int)];
        Int32Buffer.AsSpan().TryWrite(123, out _);

        Int64Buffer = new byte[sizeof(long)];
        Int64Buffer.AsSpan().TryWrite(123L, out _);

        SingleBuffer = new byte[sizeof(float)];
        SingleBuffer.AsSpan().TryWrite(123.0f, out _);

        DoubleBuffer = new byte[sizeof(double)];
        DoubleBuffer.AsSpan().TryWrite(123.0d, out _);

        StringBuffer = new byte[sizeof(int) + System.Text.Encoding.UTF8.GetByteCount("foo")];
        StringBuffer.AsSpan().TryWrite("foo", out _);

        DateTimeBuffer = new byte[sizeof(long)];
        DateTimeBuffer.AsSpan().TryWrite(_date, out _);

        DateTimeOffsetBuffer = new byte[sizeof(long) + sizeof(short)];
        DateTimeOffsetBuffer.AsSpan().TryWrite(_dateOffset, out _);

        PersistableBuffer = new byte[_persistable.GetPersistedSizeInBytes()];
        PersistableBuffer.AsSpan().TryWrite(_persistable, out _);
    }

    [BenchmarkCategory("Serialize"), Benchmark(Baseline = true)]
    public bool SerializeByte()
        => _buffer.AsSpan().TryWrite((byte)123, out _);

    [BenchmarkCategory("Serialize"), Benchmark]
    public bool SerializeInt32()
        => _buffer.AsSpan().TryWrite(123, out _);

    [BenchmarkCategory("Serialize"), Benchmark]
    public bool SerializeInt64()
        => _buffer.AsSpan().TryWrite(123L, out _);

    [BenchmarkCategory("Serialize"), Benchmark]
    public bool SerializeSingle()
        => _buffer.AsSpan().TryWrite(123.0f, out _);

    [BenchmarkCategory("Serialize"), Benchmark]
    public bool SerializeDouble()
        => _buffer.AsSpan().TryWrite(123.0d, out _);

    [BenchmarkCategory("Serialize"), Benchmark]
    public bool SerializeString()
        => _buffer.AsSpan().TryWrite("foo", out _);

    [BenchmarkCategory("Serialize"), Benchmark]
    public bool SerializeDateTime()
        => _buffer.AsSpan().TryWrite(_date, out _);

    [BenchmarkCategory("Serialize"), Benchmark]
    public bool SerializeDateTimeOffset()
        => _buffer.AsSpan().TryWrite(_dateOffset, out _);

    [BenchmarkCategory("Serialize"), Benchmark] 
    public bool SerializePersistable()
        => _buffer.AsSpan().TryWrite(_persistable, out _);



    [BenchmarkCategory("Deserialize"), Benchmark(Baseline = true)]
    public bool DeserializeByte()
        => new ReadOnlySpan<byte>(ByteBuffer).TryRead(out byte value, out _);

    [BenchmarkCategory("Deserialize"), Benchmark]
    public bool DeserializeInt32()
        => new ReadOnlySpan<byte>(ByteBuffer).TryRead(out int value, out _);

    [BenchmarkCategory("Deserialize"), Benchmark]
    public bool DeserializeInt64()
        => new ReadOnlySpan<byte>(ByteBuffer).TryRead(out long value, out _);

    [BenchmarkCategory("Deserialize"), Benchmark]
    public bool DeserializeSingle()
        => new ReadOnlySpan<byte>(ByteBuffer).TryRead(out float value, out _);

    [BenchmarkCategory("Deserialize"), Benchmark]
    public bool DeserializeDouble()
        => new ReadOnlySpan<byte>(ByteBuffer).TryRead(out double value, out _);

    [BenchmarkCategory("Deserialize"), Benchmark]
    public bool DeserializeString()
        => new ReadOnlySpan<byte>(ByteBuffer).TryRead(out string value, out _);

    [BenchmarkCategory("Deserialize"), Benchmark]
    public bool DeserializeDateTime()
        => new ReadOnlySpan<byte>(ByteBuffer).TryRead(out DateTime value, out _);

    [BenchmarkCategory("Deserialize"), Benchmark]
    public bool DeserializeDateTimeOffset()
        => new ReadOnlySpan<byte>(ByteBuffer).TryRead(out DateTimeOffset value, out _);

    [BenchmarkCategory("Deserialize"), Benchmark]
    public bool DeserializePersistable()
        => new ReadOnlySpan<byte>(PersistableBuffer).TryRead(out TestPersistable? value, out _);


    public record TestPersistable : IPersistable<TestPersistable> {
        public static bool TryRead(ReadOnlySpan<byte> from, out TestPersistable? value, out ReadOnlySpan<byte> rest)
            => InitializeOuts(out value, out rest)
            && from.TryRead(out int integerProp, out rest)
            && rest.TryRead(out string stringProp, out rest, Encoding.UTF8)
            && CreateInstance(integerProp, stringProp, out value);

        public int IntegerProperty { get; init; }

        public string StringProperty { get; init; } = string.Empty;

        public int GetPersistedSizeInBytes() => sizeof(int) + sizeof(int) + Encoding.UTF8.GetByteCount(StringProperty);

        public bool TryWrite(Span<byte> to, out Span<byte> rest)
            => to.TryWrite(IntegerProperty, out rest)
            && rest.TryWrite(StringProperty, out rest, Encoding.UTF8);

        private static bool InitializeOuts(out TestPersistable? persistable, out ReadOnlySpan<byte> rest) {
            persistable = default;
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