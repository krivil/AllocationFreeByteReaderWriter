namespace AllocationFreeByteReaderWriter {
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static partial class ByteWriter {
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
        private const byte _trueAsByte = 1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, bool value, out Span<byte> rest) {
            if (destination.Length < ByteLengthInBytes) {
                rest = destination;
                return false;
            }

            destination[0] = value ? _trueAsByte : _falseAsByte;
            rest = destination[ByteLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, byte value, out Span<byte> rest) {
            if (destination.Length < ByteLengthInBytes) {
                rest = destination;
                return false;
            }

            destination[0] = value;
            rest = destination[ByteLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, sbyte value, out Span<byte> rest) {
            if (destination.Length < SByteLengthInBytes) {
                rest = destination;
                return false;
            }

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            rest = destination[SByteLengthInBytes..];
            return true;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, short value, out Span<byte> rest) {
            if (destination.Length < ShortLengthInBytes) {
                rest = destination;
                return false;
            }

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            rest = destination[ShortLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, ushort value, out Span<byte> rest) {
            if (destination.Length < UShortLengthInBytes) {
                rest = destination;
                return false;
            }

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            rest = destination[UShortLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, int value, out Span<byte> rest) {
            if (destination.Length < IntLengthInBytes) {
                rest = destination;
                return false;
            }

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            rest = destination[IntLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, uint value, out Span<byte> rest) {
            if (destination.Length < UIntLengthInBytes) {
                rest = destination;
                return false;
            }

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            rest = destination[UIntLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, long value, out Span<byte> rest) {
            if (destination.Length < LongLengthInBytes) {
                rest = destination;
                return false;
            }

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            rest = destination[LongLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, ulong value, out Span<byte> rest) {
            if (destination.Length < ULongLengthInBytes) {
                rest = destination;
                return false;
            }

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            rest = destination[ULongLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, char value, out Span<byte> rest) {
            if (destination.Length < CharLengthInBytes) {
                rest = destination;
                return false;
            }

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            rest = destination[CharLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, float value, out Span<byte> rest) {
            if (destination.Length < FloatLengthInBytes) {
                rest = destination;
                return false;
            }

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            rest = destination[FloatLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, double value, out Span<byte> rest) {
            if (destination.Length < DoubleLengthInBytes) {
                rest = destination;
                return false;
            }

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            rest = destination[DoubleLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in decimal value, out Span<byte> rest) {
            if (destination.Length < DecimalLengthInBytes) {
                rest = destination;
                return false;
            }

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            rest = destination[DecimalLengthInBytes..];
            return true;
        }

    }
}