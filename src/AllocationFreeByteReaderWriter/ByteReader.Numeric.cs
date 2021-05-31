namespace AllocationFreeByteReaderWriter {
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static partial class ByteReader {
        public const int ByteLengthInBytes = sizeof(byte);
        public const int SByteLengthInBytes = sizeof(sbyte);
        public const int ShortLengthInBytes = sizeof(short);
        public const int UShortLengthInBytes = sizeof(ushort);
        public const int IntLengthInBytes = sizeof(int);
        public const int UIntLengthInBytes = sizeof(uint);
        public const int LongLengthInBytes = sizeof(long);
        public const int ULongLengthInBytes = sizeof(ulong);
        public const int CharLengthInBytes = sizeof(char);
        public const int FloatLengthInBytes = sizeof(float);
        public const int DoubleLengthInBytes = sizeof(double);
        public const int DecimalLengthInBytes = sizeof(decimal);

        private const byte _falseAsByte = 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRead(this in ReadOnlySpan<byte> source, out bool value, out ReadOnlySpan<byte> rest) {
            if (source.Length < ByteLengthInBytes) {
                value = default;
                rest = source;
                return false;
            }

            value = source[0] != _falseAsByte;
            rest = source[ByteLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRead(this in ReadOnlySpan<byte> source, out byte value, out ReadOnlySpan<byte> rest) {
            if (source.Length < ByteLengthInBytes) {
                value = default;
                rest = source;
                return false;
            }

            value = source[0];
            rest = source[ByteLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRead(this in ReadOnlySpan<byte> source, out sbyte value, out ReadOnlySpan<byte> rest) {
            if (source.Length < SByteLengthInBytes) {
                value = default;
                rest = source;
                return false;
            }

            value = Unsafe.ReadUnaligned<sbyte>(ref MemoryMarshal.GetReference(source));
            rest = source[SByteLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRead(this in ReadOnlySpan<byte> source, out short value, out ReadOnlySpan<byte> rest) {
            if (source.Length < ShortLengthInBytes) {
                value = default;
                rest = source;
                return false;
            }

            ReadOnlySpan<byte> valueSpan = source[..ShortLengthInBytes];
            value = Unsafe.ReadUnaligned<short>(ref MemoryMarshal.GetReference(valueSpan));
            rest = source[ShortLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRead(this in ReadOnlySpan<byte> source, out ushort value, out ReadOnlySpan<byte> rest) {
            if (source.Length < UShortLengthInBytes) {
                value = default;
                rest = source;
                return false;
            }

            ReadOnlySpan<byte> valueSpan = source[..UShortLengthInBytes];
            value = Unsafe.ReadUnaligned<ushort>(ref MemoryMarshal.GetReference(valueSpan));
            rest = source[UShortLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRead(this in ReadOnlySpan<byte> source, out int value, out ReadOnlySpan<byte> rest) {
            if (source.Length < IntLengthInBytes) {
                value = default;
                rest = source;
                return false;
            }

            ReadOnlySpan<byte> valueSpan = source[..IntLengthInBytes];
            value = Unsafe.ReadUnaligned<int>(ref MemoryMarshal.GetReference(valueSpan));
            rest = source[IntLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRead(this in ReadOnlySpan<byte> source, out uint value, out ReadOnlySpan<byte> rest) {
            if (source.Length < UIntLengthInBytes) {
                value = default;
                rest = source;
                return false;
            }

            ReadOnlySpan<byte> valueSpan = source[..UIntLengthInBytes];
            value = Unsafe.ReadUnaligned<uint>(ref MemoryMarshal.GetReference(valueSpan));
            rest = source[UIntLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRead(this in ReadOnlySpan<byte> source, out long value, out ReadOnlySpan<byte> rest) {
            if (source.Length < LongLengthInBytes) {
                value = default;
                rest = source;
                return false;
            }

            ReadOnlySpan<byte> valueSpan = source[..LongLengthInBytes];
            value = Unsafe.ReadUnaligned<long>(ref MemoryMarshal.GetReference(valueSpan));
            rest = source[LongLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRead(this in ReadOnlySpan<byte> source, out ulong value, out ReadOnlySpan<byte> rest) {
            if (source.Length < ULongLengthInBytes) {
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
        public static bool TryRead(this in ReadOnlySpan<byte> source, out char value, out ReadOnlySpan<byte> rest) {
            if (source.Length < CharLengthInBytes) {
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
        public static bool TryRead(this in ReadOnlySpan<byte> source, out float value, out ReadOnlySpan<byte> rest) {
            if (source.Length < FloatLengthInBytes) {
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
        public static bool TryRead(this in ReadOnlySpan<byte> source, out double value, out ReadOnlySpan<byte> rest) {
            if (source.Length < DoubleLengthInBytes) {
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
        public static bool TryRead(this in ReadOnlySpan<byte> source, out decimal value, out ReadOnlySpan<byte> rest) {
            if (source.Length < DecimalLengthInBytes) {
                value = default;
                rest = source;
                return false;
            }

            ReadOnlySpan<byte> valueSpan = source[..DecimalLengthInBytes];
            value = Unsafe.ReadUnaligned<decimal>(ref MemoryMarshal.GetReference(valueSpan));
            rest = source[DecimalLengthInBytes..];
            return true;
        }
    }
}