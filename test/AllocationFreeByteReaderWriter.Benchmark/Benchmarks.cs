using BenchmarkDotNet.Attributes;
using AllocationFreeByteReaderWriter;
using BenchmarkDotNet.Configs;
using AllocationFreeByteReaderWriter.Persistable;
using System;
using System.Text;
using System.Text.Json;
using System.Diagnostics.CodeAnalysis;

[MemoryDiagnoser]
[CsvMeasurementsExporter]
[RPlotExporter]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class Benchmarks {
    private static readonly byte[] _buffer = new byte[1024];
    private static readonly DateTime _date = DateTime.Now;
    private static readonly DateTimeOffset _dateOffset = DateTimeOffset.Now;
    private static readonly TestPersistable _persistable = new(){IntegerProperty = 123, StringProperty = "foo"};

    private byte[] _byteBuffer = Array.Empty<byte>();
    private byte[] _int32Buffer = Array.Empty<byte>();
    private byte[] _int64Buffer = Array.Empty<byte>();
    private byte[] _singleBuffer = Array.Empty<byte>();
    private byte[] _doubleBuffer = Array.Empty<byte>();
    private byte[] _stringBuffer = Array.Empty<byte>();
    private byte[] _dateTimeBuffer = Array.Empty<byte>();
    private byte[] _dateTimeOffsetBuffer = Array.Empty<byte>();
    private byte[] _persistableBuffer = Array.Empty<byte>();

    [GlobalSetup]
    public void GlobalSetup() {
        _byteBuffer = new byte[1];
        _byteBuffer.AsSpan().TryWrite((byte)123, out _);

        _int32Buffer = new byte[sizeof(int)];
        _int32Buffer.AsSpan().TryWrite(123, out _);

        _int64Buffer = new byte[sizeof(long)];
        _int64Buffer.AsSpan().TryWrite(123L, out _);

        _singleBuffer = new byte[sizeof(float)];
        _singleBuffer.AsSpan().TryWrite(123.0f, out _);

        _doubleBuffer = new byte[sizeof(double)];
        _doubleBuffer.AsSpan().TryWrite(123.0d, out _);

        _stringBuffer = new byte[sizeof(int) + System.Text.Encoding.UTF8.GetByteCount("foo")];
        _stringBuffer.AsSpan().TryWrite("foo", out _);

        _dateTimeBuffer = new byte[sizeof(long)];
        _dateTimeBuffer.AsSpan().TryWrite(_date, out _);

        _dateTimeOffsetBuffer = new byte[sizeof(long) + sizeof(short)];
        _dateTimeOffsetBuffer.AsSpan().TryWrite(_dateOffset, out _);

        _persistableBuffer = new byte[_persistable.GetPersistedSizeInBytes()];
        _persistableBuffer.AsSpan().TryWrite(_persistable, out _);
    }

    [BenchmarkCategory("Serialize,AllocationFree"), Benchmark(Baseline = true)]
    public bool SerializeByte()
        => _buffer.AsSpan().TryWrite((byte)123, out _);

    [BenchmarkCategory("Serialize,Json"), Benchmark(Baseline = true)]
    public string SerializeByteJson()
        => JsonSerializer.Serialize((byte)123);

    [BenchmarkCategory("Serialize,AllocationFree"), Benchmark]
    public bool SerializeInt32()
        => _buffer.AsSpan().TryWrite(123, out _);

    [BenchmarkCategory("Serialize,Json"), Benchmark]
    public string SerializeInt32Json()
        => JsonSerializer.Serialize(123);

    [BenchmarkCategory("Serialize,AllocationFree"), Benchmark]
    public bool SerializeInt64()
        => _buffer.AsSpan().TryWrite(123L, out _);

    [BenchmarkCategory("Serialize,Json"), Benchmark]
    public string SerializeInt64Json()
        => JsonSerializer.Serialize(123L);

    [BenchmarkCategory("Serialize,AllocationFree"), Benchmark]
    public bool SerializeSingle()
        => _buffer.AsSpan().TryWrite(123.0f, out _);

    [BenchmarkCategory("Serialize,Json"), Benchmark]
    public string SerializeSingleJson()
        => JsonSerializer.Serialize(123.0f);

    [BenchmarkCategory("Serialize,AllocationFree"), Benchmark]
    public bool SerializeDouble()
        => _buffer.AsSpan().TryWrite(123.0d, out _);

    [BenchmarkCategory("Serialize,Json"), Benchmark]
    public string SerializeDoubleJson()
        => JsonSerializer.Serialize(123.0d);

    [BenchmarkCategory("Serialize,AllocationFree"), Benchmark]
    public bool SerializeString()
        => _buffer.AsSpan().TryWrite("foo", out _);

    [BenchmarkCategory("Serialize,Json"), Benchmark]
    public string SerializeStringJson()
        => JsonSerializer.Serialize("foo");

    [BenchmarkCategory("Serialize,AllocationFree"), Benchmark]
    public bool SerializeDateTime()
        => _buffer.AsSpan().TryWrite(_date, out _);

    [BenchmarkCategory("Serialize,Json"), Benchmark]
    public string SerializeDateTimeJson()
        => JsonSerializer.Serialize(_date);

    [BenchmarkCategory("Serialize,AllocationFree"), Benchmark]
    public bool SerializeDateTimeOffset()
        => _buffer.AsSpan().TryWrite(_dateOffset, out _);

    [BenchmarkCategory("Serialize,Json"), Benchmark]
    public string SerializeDateTimeOffsetJson()
        => JsonSerializer.Serialize(_dateOffset);

    [BenchmarkCategory("Serialize,AllocationFree"), Benchmark]
    public bool SerializePersistable()
        => _buffer.AsSpan().TryWrite(_persistable, out _);

    [BenchmarkCategory("Serialize,Json"), Benchmark]
    public string SerializePersistableJson()
        => JsonSerializer.Serialize(_persistable);



    [BenchmarkCategory("Deserialize,AllocationFree"), Benchmark(Baseline = true)]
    public bool DeserializeByte()
        => new ReadOnlySpan<byte>(_byteBuffer).TryRead(out byte value, out _);

    [BenchmarkCategory("Deserialize,Json"), Benchmark(Baseline = true)]
    public byte DeserializeByteJson()
        => JsonSerializer.Deserialize<byte>("123");


    [BenchmarkCategory("Deserialize,AllocationFree"), Benchmark]
    public bool DeserializeInt32()
        => new ReadOnlySpan<byte>(_byteBuffer).TryRead(out int value, out _);

    [BenchmarkCategory("Deserialize,Json"), Benchmark]
    public int DeserializeInt32Json()
        => JsonSerializer.Deserialize<int>("123");

    [BenchmarkCategory("Deserialize,AllocationFree"), Benchmark]
    public bool DeserializeInt64()
        => new ReadOnlySpan<byte>(_byteBuffer).TryRead(out long value, out _);

    [BenchmarkCategory("Deserialize,Json"), Benchmark]
    public long DeserializeInt64Json()
        => JsonSerializer.Deserialize<long>("123");

    [BenchmarkCategory("Deserialize,AllocationFree"), Benchmark]
    public bool DeserializeSingle()
        => new ReadOnlySpan<byte>(_byteBuffer).TryRead(out float value, out _);

    [BenchmarkCategory("Deserialize,Json"), Benchmark]
    public float DeserializeSingleJson()
        => JsonSerializer.Deserialize<float>("123.0");

    [BenchmarkCategory("Deserialize,AllocationFree"), Benchmark]
    public bool DeserializeDouble()
        => new ReadOnlySpan<byte>(_byteBuffer).TryRead(out double value, out _);

    [BenchmarkCategory("Deserialize,Json"), Benchmark]
    public double DeserializeDoubleJson()
        => JsonSerializer.Deserialize<double>("123.0");

    [BenchmarkCategory("Deserialize,AllocationFree"), Benchmark]
    public bool DeserializeString()
        => new ReadOnlySpan<byte>(_byteBuffer).TryRead(out string value, out _);

    [BenchmarkCategory("Deserialize,Json"), Benchmark]
    public string? DeserializeStringJson()
        => JsonSerializer.Deserialize<string>("\"foo\"");

    [BenchmarkCategory("Deserialize,AllocationFree"), Benchmark]
    public bool DeserializeDateTime()
        => new ReadOnlySpan<byte>(_byteBuffer).TryRead(out DateTime value, out _);

    [BenchmarkCategory("Deserialize,Json"), Benchmark]
    public DateTime DeserializeDateTimeJson()
        => JsonSerializer.Deserialize<DateTime>("\"2021-01-01T23:59:59\"");

    [BenchmarkCategory("Deserialize,AllocationFree"), Benchmark]
    public bool DeserializeDateTimeOffset()
        => new ReadOnlySpan<byte>(_byteBuffer).TryRead(out DateTimeOffset value, out _);

    [BenchmarkCategory("Deserialize,Json"), Benchmark]
    public DateTimeOffset DeserializeDateTimeOffsetJson()
        => JsonSerializer.Deserialize<DateTimeOffset>("\"2021-01-01T23:59:59+03:00\"");

    [BenchmarkCategory("Deserialize,AllocationFree"), Benchmark]
    public bool DeserializePersistable()
        => ((ReadOnlySpan<byte>)_persistableBuffer).TryRead(out TestPersistable _, out _);

    [BenchmarkCategory("Deserialize,Json"), Benchmark]
    public TestPersistable? DeserializePersistableJson()
        => JsonSerializer.Deserialize<TestPersistable>("{\"IntegerProperty\": 123, \"StringProperty\": \"foo\"}");


    public sealed record TestPersistable : IPersistable<TestPersistable> {
        public static bool TryRead(ReadOnlySpan<byte> from, out TestPersistable value, out ReadOnlySpan<byte> rest)
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