namespace AllocationFreeByteReaderWriter.Serializable;

using System;
using System.Runtime.CompilerServices;

public interface IByteWritable {
    int CalculateSizeInBytes();
    
    bool TryWrite(Span<byte> to, out Span<byte> rest);

    public byte[] ToBytes() {
        byte[] result = new byte[CalculateSizeInBytes()];
        return TryWrite(result.AsSpan(), out _)
            ? result
            : throw new Exception("Unable to convert to bytes");
    }
}

public interface IByteWritable<T> : IByteWritable where T : IByteWritable<T> {
    public abstract static bool TryRead(ReadOnlySpan<byte> from, out T? value, out ReadOnlySpan<byte> rest);
}

public record TestByteWritable(string Name, int Age) : IByteWritable<TestByteWritable> {
    //public static bool TryRead(ReadOnlySpan<byte> from, out TestByteWritable? value, out ReadOnlySpan<byte> rest) =>
    //    InitializeHelpers.Init(out value, out rest)
    //    && from.TryRead(out string name, out rest)
    //    && rest.TryRead(out int age, out rest)
    //    && HelpCreate(name, age, out value)
    //    //&& (value = new TestByteWritable {Age = age, Name = name}) != null
    //    ;

    public static bool TryRead(ReadOnlySpan<byte> from, out TestByteWritable? value, out ReadOnlySpan<byte> rest) {
        value = default; int age = default;
        bool success = from.TryRead(out string name, out rest)
               && rest.TryRead(out age, out rest);
        if (!success) return false;
        value = new TestByteWritable(name, age);
        return true;
    }

    public int CalculateSizeInBytes() => ByteWriter.GetSize(Name) + ByteWriter.GetSize(Age);

    public bool TryWrite(Span<byte> to, out Span<byte> rest) =>
        to.TryWrite(Name, out rest) && rest.TryWrite(Age, out rest);

    private static bool HelpCreate(string name, int age, out TestByteWritable value) {
        value = new TestByteWritable(name, age);
        return true;
    }
}

internal static class InitializeHelpers {
#nullable disable
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Init(out ReadOnlySpan<byte> rest) {
        rest = ReadOnlySpan<byte>.Empty;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Init<T>(out T obj) {
        obj = default;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Init<T1>(out T1 obj1, out ReadOnlySpan<byte> rest) {
        obj1 = default;
        rest = ReadOnlySpan<byte>.Empty;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Init<T1, T2>(out T1 obj1, out T2 obj2) {
        obj1 = default;
        obj2 = default;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Init<T1, T2>(out T1 obj1, out T2 obj2, out ReadOnlySpan<byte> rest) {
        obj1 = default;
        obj2 = default;
        rest = ReadOnlySpan<byte>.Empty;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Init<T1, T2, T3>(out T1 obj1, out T2 obj2, out T3 obj3) {
        obj1 = default;
        obj2 = default;
        obj3 = default;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Init<T1, T2, T3>(out T1 obj1, out T2 obj2, out T3 obj3, out ReadOnlySpan<byte> rest) {
        obj1 = default;
        obj2 = default;
        obj3 = default;
        rest = ReadOnlySpan<byte>.Empty;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Init<T1, T2, T3, T4>(out T1 obj1, out T2 obj2, out T3 obj3, out T4 obj4) {
        obj1 = default;
        obj2 = default;
        obj3 = default;
        obj4 = default;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Init<T1, T2, T3, T4>(out T1 obj1, out T2 obj2, out T3 obj3, out T4 obj4, out ReadOnlySpan<byte> rest) {
        obj1 = default;
        obj2 = default;
        obj3 = default;
        obj4 = default;
        rest = ReadOnlySpan<byte>.Empty;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Init<T1, T2, T3, T4, T5>(out T1 obj1, out T2 obj2, out T3 obj3, out T4 obj4, out T5 obj5) {
        obj1 = default;
        obj2 = default;
        obj3 = default;
        obj4 = default;
        obj5 = default;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Init<T1, T2, T3, T4, T5>(out T1 obj1, out T2 obj2, out T3 obj3, out T4 obj4, out T5 obj5, out ReadOnlySpan<byte> rest) {
        obj1 = default;
        obj2 = default;
        obj3 = default;
        obj4 = default;
        obj5 = default;
        rest = ReadOnlySpan<byte>.Empty;
        return true;
    }
#nullable restore
}


public interface IBinarySerializable {
    int CalculateSizeForBytesSerialization();
    bool ToBytes(Span<byte> bytes, out Span<byte> rest);
}

public interface IPersistable<T> where T : IPersistable<T> {
    abstract static bool TryRead(ReadOnlySpan<byte> from, out T? value, out ReadOnlySpan<byte> rest);

    int GetPersistedSizeInBytes();
    bool TryWrite(Span<byte> to, out Span<byte> rest);
}

public static class Persistable {
    private static class TypeCodes {
        internal const byte ADDRESS = 0xFF;

        internal const byte ADDRESSA = 0xFE;

        internal const byte INT16 = 0xFD;

        internal const byte INT16A = 0xFC;

        internal const byte INT32 = 0xFB;

        internal const byte INT32A = 0xFA;

        internal const byte INT2D = 0xF9;

        internal const byte INT64 = 0xF8;

        internal const byte INT64A = 0xF7;

        internal const byte BYTE = 0xF6;

        internal const byte BYTEA = 0xF5;

        internal const byte BOOL = 0xF4;

        internal const byte BOOLA = 0xF3;

        internal const byte STRING = 0xF2;

        internal const byte STRINGA = 0xF1;

        internal const byte FLOAT = 0xF0;

        internal const byte FLOATA = 0xEF;

        internal const byte FLOAT2D = 0xEE;

        internal const byte DOUBLE = 0xED;

        internal const byte DOUBLEA = 0xEC;

        internal const byte DOUBLE2D = 0xEB;

        internal const byte CHAR = 0xEA;

        internal const byte CHARA = 0xE9;

        internal const byte UINT16 = 0xE8;

        internal const byte UINT16A = 0xE7;

        internal const byte UINT32 = 0xE6;

        internal const byte UINT32A = 0xE5;

        internal const byte UINT64 = 0xE4;

        internal const byte UINT64A = 0xE3;

        internal const byte MESSAGE = 0xE2;

        internal const byte VIEW = 0xE1;

        internal const byte VIEWDELTA = 0xE0;

        internal const byte NESTED = 0xDF;

        internal const byte NESTED0 = 0xDE;

        internal const byte NULL = 0xDD;

        internal const byte UNSTABLE = 0xDC;

        internal const byte TOKENINFO = 0xDB;

        internal const byte GRPAIR = 0xDA;

        internal const byte FLSHAGGKEY = 0xD0;

        internal const byte BVS = 0xD9;

        internal const byte LOCKINFO = 0xD7;

        internal const byte KEYVALUEP = 0xD6;

        internal const byte LIST = 0xD5;

        internal const byte QUERYKEY = 0xD4;

        internal const byte DHTITEM = 0xD3;

        internal const byte OSSPQ = 0xD2;

        internal const byte LISTO = 0xD1;

        internal const byte OOBREPINFO = 0xCF;

        internal const byte LOCKREQ = 0xCE;

        internal const byte IPADDR = 0xCD;

        internal const byte IPADDRA = 0xCC;

        internal const byte UNDEF = 0xCB;
    }
    public static  int GetPersistedSizeInBytes<T>(this IPersistable<T> persistable)
        where T : IPersistable<T> { throw new NotImplementedException(); }
    public static bool TryWrite<T>(this IPersistable<T> persistable, Span<byte> to, out Span<byte> rest)
        where T : IPersistable<T> { throw new NotImplementedException(); }

    public static bool TryWriteNull(Span<byte> to, out Span<byte> rest) { throw new NotImplementedException(); }


}
