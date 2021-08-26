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
    public static bool TryRead(ReadOnlySpan<byte> span, out TestPersistable value, out ReadOnlySpan<byte> rest)
        => InitializeOuts(out value, out rest)
        && span.TryRead(out int integerProp, out rest)
        && rest.TryRead(out string stringProp, out rest, Encoding.UTF8)
        && CreateInstance(integerProp, stringProp, out value);

    public int IntegerProperty { get; init; }
    
    public string StringProperty { get; init; }

    // interface override
    public int GetPersistedSizeInBytes() => sizeof(int) + sizeof(int) + Encoding.UTF8.GetByteCount(StringProperty);
    
    // interface override
    public bool TryWrite(Span<byte> span, out Span<byte> rest)
        => span.TryWrite(IntegerProperty, out rest)
        && rest.TryWrite(StringProperty, out rest, Encoding.UTF8);

    // helper method for TryRead
    private static bool InitializeOuts(out TestPersistable persistable, out ReadOnlySpan<byte> rest) {
        persistable = default;
        rest = default;
        return true;
    }

    // helper method for TryRead
    private static bool CreateInstance(int integerProperty, string stringProperty, out TestPersistable persistable) {
        persistable = new TestPersistable() {
            IntegerProperty = integerProperty,
            StringProperty = stringProperty,
        };
        return true;
    }
}
```

```csharp
bool success = span.TryWrite(new TestPersistable(){ IntegerProperty = 1, StringProperty = "foo" }, out Span<byte> rest);
```

```csharp
bool success = span.TryRead(out TestPersistable persistableValue, out Span<byte> rest);
```

## License
[Apache License 2.0](https://choosealicense.com/licenses/apache-2.0/)