# AllocationFreeByteReaderWriter

A library for binary serialization / deserialization without *(unnecessary)* allocation in C#

## Usage with included types

```csharp
bool success = span.TryWrite(IntValue, out Span<byte> rest) 
            && rest.TryWrite(LongValue, out rest) 
            && rest.TryWrite(ShortValue, out _);
```

```csharp
bool success = span.TryRead(out int intValue, out Span<byte> rest)
            && rest.TryRead(out long longValue, out rest)
            && rest.TryRead(out short shortValue, out _);
```

## Create new type

Requrements:
 - .NET 6
 - C# 10 preview

```csharp
public record TestPersistable : IPersistable<TestPersistable> {
    // abstract static intefrace override
    public static bool TryRead(
        ReadOnlySpan<byte> span,
        out TestPersistable value,
        out ReadOnlySpan<byte> rest)

        => InitializeOuts(out value, out rest)
        && span.TryRead(out int integerProp, out rest)
        && rest.TryRead(out string stringProp, out rest, Encoding.UTF8)
        && CreateInstance(integerProp, stringProp, out value);

    public int IntegerProperty { get; init; }
    
    public string StringProperty { get; init; }

    // interface override
    public int GetPersistedSizeInBytes() 
        => sizeof(int)
        + sizeof(int) + Encoding.UTF8.GetByteCount(StringProperty);
    
    // interface override
    public bool TryWrite(Span<byte> span, out Span<byte> rest)
        => span.TryWrite(IntegerProperty, out rest)
        && rest.TryWrite(StringProperty, out rest, Encoding.UTF8);

    // helper method for TryRead
    private static bool InitializeOuts(
        out TestPersistable persistable,
        out ReadOnlySpan<byte> rest) {

        persistable = default;
        rest = default;
        return true;
    }

    // helper method for TryRead
    private static bool CreateInstance(
        int integerProperty,
        string stringProperty,
        out TestPersistable persistable) {

        persistable = new TestPersistable() {
            IntegerProperty = integerProperty,
            StringProperty = stringProperty,
        };
        return true;
    }
}
```

```csharp
var persistable = new TestPersistable(){ IntegerProperty = 1, StringProperty = "foo" };
bool success = span.TryWrite(persistable, out Span<byte> rest);
```

```csharp
bool success = span.TryRead(out TestPersistable persistableValue, out Span<byte> rest);
```

## Benchmark

``` ini

BenchmarkDotNet=v0.13.1, OS=ubuntu 20.04
Intel Core i9-9900K CPU 3.60GHz (Coffee Lake), 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.100-preview.7.21379.14
  [Host]     : .NET 6.0.0 (6.0.21.37719), X64 RyuJIT
  DefaultJob : .NET 6.0.0 (6.0.21.37719), X64 RyuJIT


```
|                    Method |  Categories |        Mean |     Error |    StdDev |      Median |   Ratio | RatioSD | Completed Work Items | Lock Contentions |  Gen 0 | Allocated |
|-------------------------- |------------ |------------:|----------:|----------:|------------:|--------:|--------:|---------------------:|-----------------:|-------:|----------:|
|             SerializeByte |   Serialize |   0.4633 ns | 0.0047 ns | 0.0039 ns |   0.4642 ns |    1.00 |    0.00 |                    - |                - |      - |         - |
|            SerializeInt32 |   Serialize |   0.4600 ns | 0.0044 ns | 0.0037 ns |   0.4600 ns |    0.99 |    0.01 |                    - |                - |      - |         - |
|            SerializeInt64 |   Serialize |   0.4626 ns | 0.0045 ns | 0.0038 ns |   0.4620 ns |    1.00 |    0.01 |                    - |                - |      - |         - |
|           SerializeSingle |   Serialize |   0.5379 ns | 0.0192 ns | 0.0180 ns |   0.5311 ns |    1.16 |    0.04 |                    - |                - |      - |         - |
|           SerializeDouble |   Serialize |   0.4970 ns | 0.0029 ns | 0.0024 ns |   0.4964 ns |    1.07 |    0.01 |                    - |                - |      - |         - |
|           SerializeString |   Serialize |  73.8650 ns | 0.5056 ns | 0.4730 ns |  73.9883 ns |  159.42 |    1.31 |                    - |                - | 0.0038 |      32 B |
|         SerializeDateTime |   Serialize | 195.1607 ns | 1.0355 ns | 0.8084 ns | 195.0271 ns |  421.69 |    3.63 |                    - |                - |      - |         - |
|   SerializeDateTimeOffset |   Serialize |  10.6000 ns | 0.2379 ns | 0.5655 ns |  10.3932 ns |   24.16 |    0.96 |                    - |                - |      - |         - |
|      SerializePersistable |   Serialize |  73.8867 ns | 0.5216 ns | 0.4072 ns |  73.7765 ns |  159.66 |    1.94 |                    - |                - | 0.0038 |      32 B |
|                           |             |             |           |           |             |         |         |                      |                  |        |           |
|           DeserializeByte | Deserialize |   0.2916 ns | 0.0063 ns | 0.0053 ns |   0.2930 ns |   1.000 |    0.00 |                    - |                - |      - |         - |
|          DeserializeInt32 | Deserialize |   0.2655 ns | 0.0083 ns | 0.0077 ns |   0.2654 ns |   0.911 |    0.03 |                    - |                - |      - |         - |
|          DeserializeInt64 | Deserialize |   0.2761 ns | 0.0044 ns | 0.0039 ns |   0.2757 ns |   0.947 |    0.02 |                    - |                - |      - |         - |
|         DeserializeSingle | Deserialize |   0.2562 ns | 0.0030 ns | 0.0025 ns |   0.2565 ns |   0.879 |    0.02 |                    - |                - |      - |         - |
|         DeserializeDouble | Deserialize |   0.0000 ns | 0.0000 ns | 0.0000 ns |   0.0000 ns |   0.000 |    0.00 |                    - |                - |      - |         - |
|         DeserializeString | Deserialize |   1.3641 ns | 0.0064 ns | 0.0056 ns |   1.3631 ns |   4.677 |    0.09 |                    - |                - |      - |         - |
|       DeserializeDateTime | Deserialize |   0.2634 ns | 0.0112 ns | 0.0105 ns |   0.2621 ns |   0.895 |    0.03 |                    - |                - |      - |         - |
| DeserializeDateTimeOffset | Deserialize |   1.2283 ns | 0.0118 ns | 0.0104 ns |   1.2286 ns |   4.219 |    0.09 |                    - |                - |      - |         - |
|    DeserializePersistable | Deserialize |  45.0231 ns | 0.2592 ns | 0.2298 ns |  44.9424 ns | 154.425 |    3.32 |                    - |                - | 0.0076 |      64 B |


## License
[Apache License 2.0](https://choosealicense.com/licenses/apache-2.0/)