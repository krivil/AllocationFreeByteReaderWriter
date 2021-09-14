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

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.22000
Intel Core i9-9900K CPU 3.60GHz (Coffee Lake), 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.100-preview.7.21379.14
  [Host]     : .NET 6.0.0 (6.0.21.37719), X64 RyuJIT
  DefaultJob : .NET 6.0.0 (6.0.21.37719), X64 RyuJIT


```
|                        Method |                 Categories |        Mean |     Error |    StdDev |  Ratio | RatioSD |  Gen 0 | Allocated |
|------------------------------ |--------------------------- |------------:|----------:|----------:|-------:|--------:|-------:|----------:|
|                 SerializeByte |   Serialize,AllocationFree |   0.4047 ns | 0.0031 ns | 0.0026 ns |   1.00 |    0.00 |      - |         - |
|                SerializeInt32 |   Serialize,AllocationFree |   0.4902 ns | 0.0039 ns | 0.0032 ns |   1.21 |    0.01 |      - |         - |
|                SerializeInt64 |   Serialize,AllocationFree |   0.4609 ns | 0.0064 ns | 0.0054 ns |   1.14 |    0.02 |      - |         - |
|               SerializeSingle |   Serialize,AllocationFree |   0.5561 ns | 0.0349 ns | 0.0358 ns |   1.38 |    0.10 |      - |         - |
|               SerializeDouble |   Serialize,AllocationFree |   0.5876 ns | 0.0029 ns | 0.0024 ns |   1.45 |    0.01 |      - |         - |
|               SerializeString |   Serialize,AllocationFree |  51.3988 ns | 0.1563 ns | 0.1462 ns | 127.08 |    0.92 | 0.0038 |      32 B |
|             SerializeDateTime |   Serialize,AllocationFree |  82.5058 ns | 0.1614 ns | 0.1510 ns | 203.85 |    1.46 |      - |         - |
|       SerializeDateTimeOffset |   Serialize,AllocationFree |   9.3971 ns | 0.0363 ns | 0.0340 ns |  23.23 |    0.20 |      - |         - |
|          SerializePersistable |   Serialize,AllocationFree |  60.4435 ns | 0.1597 ns | 0.1333 ns | 149.37 |    1.14 | 0.0038 |      32 B |
|                               |                            |             |           |           |        |         |        |           |
|             SerializeByteJson |             Serialize,Json | 115.4455 ns | 0.9579 ns | 0.8960 ns |   1.00 |    0.00 | 0.0219 |     184 B |
|            SerializeInt32Json |             Serialize,Json | 112.2672 ns | 0.2875 ns | 0.2549 ns |   0.97 |    0.01 | 0.0219 |     184 B |
|            SerializeInt64Json |             Serialize,Json | 111.4606 ns | 0.4073 ns | 0.3810 ns |   0.97 |    0.01 | 0.0219 |     184 B |
|           SerializeSingleJson |             Serialize,Json | 219.8054 ns | 1.1593 ns | 1.0844 ns |   1.90 |    0.02 | 0.0219 |     184 B |
|           SerializeDoubleJson |             Serialize,Json | 218.3641 ns | 1.6525 ns | 1.3799 ns |   1.89 |    0.02 | 0.0219 |     184 B |
|           SerializeStringJson |             Serialize,Json | 133.6537 ns | 0.9364 ns | 0.8301 ns |   1.16 |    0.01 | 0.0219 |     184 B |
|         SerializeDateTimeJson |             Serialize,Json | 257.4679 ns | 1.3805 ns | 1.2913 ns |   2.23 |    0.02 | 0.0296 |     248 B |
|   SerializeDateTimeOffsetJson |             Serialize,Json | 170.3939 ns | 1.3720 ns | 1.2833 ns |   1.48 |    0.02 | 0.0296 |     248 B |
|      SerializePersistableJson |             Serialize,Json | 215.6746 ns | 0.8641 ns | 0.7660 ns |   1.87 |    0.02 | 0.0324 |     272 B |
|                               |                            |             |           |           |        |         |        |           |
|               DeserializeByte | Deserialize,AllocationFree |   0.2565 ns | 0.0050 ns | 0.0046 ns |   1.00 |    0.00 |      - |         - |
|              DeserializeInt32 | Deserialize,AllocationFree |   0.4373 ns | 0.0056 ns | 0.0046 ns |   1.71 |    0.03 |      - |         - |
|              DeserializeInt64 | Deserialize,AllocationFree |   0.4604 ns | 0.0044 ns | 0.0041 ns |   1.80 |    0.03 |      - |         - |
|             DeserializeSingle | Deserialize,AllocationFree |   0.4365 ns | 0.0058 ns | 0.0051 ns |   1.71 |    0.04 |      - |         - |
|             DeserializeDouble | Deserialize,AllocationFree |   0.4396 ns | 0.0043 ns | 0.0040 ns |   1.71 |    0.04 |      - |         - |
|             DeserializeString | Deserialize,AllocationFree |   1.5146 ns | 0.0094 ns | 0.0088 ns |   5.91 |    0.11 |      - |         - |
|           DeserializeDateTime | Deserialize,AllocationFree |   0.4697 ns | 0.0047 ns | 0.0044 ns |   1.83 |    0.04 |      - |         - |
|     DeserializeDateTimeOffset | Deserialize,AllocationFree |   0.6007 ns | 0.0224 ns | 0.0209 ns |   2.34 |    0.10 |      - |         - |
|        DeserializePersistable | Deserialize,AllocationFree |  26.3492 ns | 0.1198 ns | 0.1062 ns | 102.96 |    1.82 | 0.0076 |      64 B |
|                               |                            |             |           |           |        |         |        |           |
|           DeserializeByteJson |           Deserialize,Json | 110.9197 ns | 0.5239 ns | 0.4901 ns |   1.00 |    0.00 |      - |         - |
|          DeserializeInt32Json |           Deserialize,Json | 113.4686 ns | 0.8414 ns | 0.7459 ns |   1.02 |    0.01 |      - |         - |
|          DeserializeInt64Json |           Deserialize,Json | 113.2146 ns | 0.2604 ns | 0.2033 ns |   1.02 |    0.01 |      - |         - |
|         DeserializeSingleJson |           Deserialize,Json | 158.1268 ns | 0.6575 ns | 0.6150 ns |   1.43 |    0.01 |      - |         - |
|         DeserializeDoubleJson |           Deserialize,Json | 156.9926 ns | 0.2720 ns | 0.2124 ns |   1.42 |    0.01 |      - |         - |
|         DeserializeStringJson |           Deserialize,Json | 137.1684 ns | 0.3681 ns | 0.3263 ns |   1.24 |    0.01 | 0.0038 |      32 B |
|       DeserializeDateTimeJson |           Deserialize,Json | 140.7853 ns | 0.5063 ns | 0.4736 ns |   1.27 |    0.01 |      - |         - |
| DeserializeDateTimeOffsetJson |           Deserialize,Json | 166.1167 ns | 0.5057 ns | 0.4223 ns |   1.50 |    0.01 |      - |         - |
|    DeserializePersistableJson |           Deserialize,Json | 308.3187 ns | 1.1390 ns | 1.0097 ns |   2.78 |    0.02 | 0.0076 |      64 B |


## License
[Apache License 2.0](https://choosealicense.com/licenses/apache-2.0/)