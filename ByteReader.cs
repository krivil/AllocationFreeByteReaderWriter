using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace AllocationFreeByteReaderWriter
{
    public static class ByteReader
    {
        private const int ByteLengthInBytes = sizeof(byte);
        private const int SByteLengthInBytes = sizeof(sbyte);
        private const int ShortLengthInBytes = sizeof(short);
        private const int UShortLengthInBytes = sizeof(ushort);
        private const int IntLengthInBytes = sizeof(int);
        private const int UIntLengthInBytes = sizeof(uint);
        private const int LongLengthInBytes = sizeof(long);
        private const int ULongLengthInBytes = sizeof(ulong);
        private const int CharLengthInBytes = sizeof(char);
        private const int FloatLengthInBytes = sizeof(float);
        private const int DoubleLengthInBytes = sizeof(double);
        private const int DecimalLengthInBytes = sizeof(decimal);

        private const byte FalseAsByte = 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRead(this ReadOnlySpan<byte> source, out bool value, out ReadOnlySpan<byte> rest)
        {
            if (source.Length < ByteLengthInBytes)
            {
                value = default;
                rest = source;
                return false;
            }

            value = source[0] != FalseAsByte;
            rest = source.Slice(ByteLengthInBytes);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRead(this ReadOnlySpan<byte> source, out byte value, out ReadOnlySpan<byte> rest)
        {
            if (source.Length < ByteLengthInBytes)
            {
                value = default;
                rest = source;
                return false;
            }

            value = source[0];
            rest = source.Slice(ByteLengthInBytes);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRead(this ReadOnlySpan<byte> source, out sbyte value, out ReadOnlySpan<byte> rest)
        {
            if (source.Length < SByteLengthInBytes)
            {
                value = default;
                rest = source;
                return false;
            }

            value = Unsafe.ReadUnaligned<sbyte>(ref MemoryMarshal.GetReference(source));
            rest = source.Slice(SByteLengthInBytes);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRead(this ReadOnlySpan<byte> source, out short value, out ReadOnlySpan<byte> rest)
        {
            if (source.Length < ShortLengthInBytes)
            {
                value = default;
                rest = source;
                return false;
            }

            ReadOnlySpan<byte> valueSpan = source[..ShortLengthInBytes];
            value = Unsafe.ReadUnaligned<short>(ref MemoryMarshal.GetReference(valueSpan));
            rest = source.Slice(ShortLengthInBytes);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRead(this ReadOnlySpan<byte> source, out ushort value, out ReadOnlySpan<byte> rest)
        {
            if (source.Length < UShortLengthInBytes)
            {
                value = default;
                rest = source;
                return false;
            }

            ReadOnlySpan<byte> valueSpan = source[..UShortLengthInBytes];
            value = Unsafe.ReadUnaligned<ushort>(ref MemoryMarshal.GetReference(valueSpan));
            rest = source.Slice(UShortLengthInBytes);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRead(this ReadOnlySpan<byte> source, out int value, out ReadOnlySpan<byte> rest)
        {
            if (source.Length < IntLengthInBytes)
            {
                value = default;
                rest = source;
                return false;
            }

            ReadOnlySpan<byte> valueSpan = source[..IntLengthInBytes];
            value = Unsafe.ReadUnaligned<int>(ref MemoryMarshal.GetReference(valueSpan));
            rest = source.Slice(IntLengthInBytes);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRead(this ReadOnlySpan<byte> source, out uint value, out ReadOnlySpan<byte> rest)
        {
            if (source.Length < UIntLengthInBytes)
            {
                value = default;
                rest = source;
                return false;
            }

            ReadOnlySpan<byte> valueSpan = source[..UIntLengthInBytes];
            value = Unsafe.ReadUnaligned<uint>(ref MemoryMarshal.GetReference(valueSpan));
            rest = source.Slice(UIntLengthInBytes);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRead(this ReadOnlySpan<byte> source, out long value, out ReadOnlySpan<byte> rest)
        {
            if (source.Length < LongLengthInBytes)
            {
                value = default;
                rest = source;
                return false;
            }

            ReadOnlySpan<byte> valueSpan = source[..LongLengthInBytes];
            value = Unsafe.ReadUnaligned<long>(ref MemoryMarshal.GetReference(valueSpan));
            rest = source.Slice(LongLengthInBytes);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRead(this ReadOnlySpan<byte> source, out ulong value, out ReadOnlySpan<byte> rest)
        {
            if (source.Length < ULongLengthInBytes)
            {
                value = default;
                rest = source;
                return false;
            }

            ReadOnlySpan<byte> valueSpan = source[..ULongLengthInBytes];
            value = Unsafe.ReadUnaligned<ulong>(ref MemoryMarshal.GetReference(valueSpan));
            rest = source[ULongLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRead(this ReadOnlySpan<byte> source, out char value, out ReadOnlySpan<byte> rest)
        {
            if (source.Length < CharLengthInBytes)
            {
                value = default;
                rest = source;
                return false;
            }

            ReadOnlySpan<byte> valueSpan = source[..CharLengthInBytes];
            value = Unsafe.ReadUnaligned<char>(ref MemoryMarshal.GetReference(valueSpan));
            rest = source[CharLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRead(this ReadOnlySpan<byte> source, out float value, out ReadOnlySpan<byte> rest)
        {
            if (source.Length < FloatLengthInBytes)
            {
                value = default;
                rest = source;
                return false;
            }

            ReadOnlySpan<byte> valueSpan = source[..FloatLengthInBytes];
            value = Unsafe.ReadUnaligned<float>(ref MemoryMarshal.GetReference(valueSpan));
            rest = source[FloatLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRead(this ReadOnlySpan<byte> source, out double value, out ReadOnlySpan<byte> rest)
        {
            if (source.Length < DoubleLengthInBytes)
            {
                value = default;
                rest = source;
                return false;
            }

            ReadOnlySpan<byte> valueSpan = source[..DoubleLengthInBytes];
            value = Unsafe.ReadUnaligned<double>(ref MemoryMarshal.GetReference(valueSpan));
            rest = source[DoubleLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRead(this ReadOnlySpan<byte> source, out decimal value, out ReadOnlySpan<byte> rest)
        {
            if (source.Length < DecimalLengthInBytes)
            {
                value = default;
                rest = source;
                return false;
            }

            ReadOnlySpan<byte> valueSpan = source[..DecimalLengthInBytes];
            value = Unsafe.ReadUnaligned<decimal>(ref MemoryMarshal.GetReference(valueSpan));
            rest = source[DecimalLengthInBytes..];
            return true;
        }

        /// <summary>
        /// This method copies from one span to the other. It is slow and you shouldn't use it
        /// </summary>
        /// <param name="source">Span from which to read.</param>
        /// <param name="value">Span which should be filled from source span.</param>
        /// <param name="rest">What's left from source after copying.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRead(this ReadOnlySpan<byte> source, Span<byte> value, out ReadOnlySpan<byte> rest)
        {
            if (source.Length < value.Length)
            {
                rest = source;
                return false;
            }

            source[..value.Length].CopyTo(value);
            rest = source[value.Length..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRead(this ReadOnlySpan<byte> source, out ReadOnlySpan<byte> value, int length, out ReadOnlySpan<byte> rest)
        {
            if (source.Length < length)
            {
                rest = source;
                value = Span<byte>.Empty;
                return false;
            }

            value = source[..length];
            rest = source[value.Length..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRead(this ReadOnlySpan<byte> source, out string? value, out ReadOnlySpan<byte> rest, Encoding encoding)
        {
            bool success = TryRead(source, out int length, out rest);
            if (!success || rest.Length < length)
            {
                value = default;
                return false;
            }

            value = encoding.GetString(rest[..length]);
            return true;
        }

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static bool TryRead(this ReadOnlySpan<byte> source, out DateTime value, out ReadOnlySpan<byte> rest) {
        //    if (TryRead(source, out long ticks, out rest)) {
        //        value = new DateTime(ticks, DateTimeKind.Utc);
        //        return true;
        //    }


        //    value = DateTime.MinValue;
        //    return false;
        //}
    }
}
