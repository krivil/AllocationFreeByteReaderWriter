# AllocationFreeByteReaderWriter

A library for binary serialization / deserialization without allocation in C#

## Usage

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

## License
[Apache License 2.0](https://choosealicense.com/licenses/apache-2.0/)