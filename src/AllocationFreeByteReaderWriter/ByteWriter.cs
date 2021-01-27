namespace AllocationFreeByteReaderWriter {
    using System;
    using System.Buffers;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    public static class ByteWriter {
        private const int _byteLengthInBytes = sizeof(byte);
        private const int _sByteLengthInBytes = sizeof(sbyte);
        private const int _shortLengthInBytes = sizeof(short);
        private const int _uShortLengthInBytes = sizeof(ushort);
        private const int _intLengthInBytes = sizeof(int);
        private const int _uIntLengthInBytes = sizeof(uint);
        private const int _longLengthInBytes = sizeof(long);
        private const int _uLongLengthInBytes = sizeof(ulong);
        private const int _charLengthInBytes = sizeof(char);
        private const int _floatLengthInBytes = sizeof(float);
        private const int _doubleLengthInBytes = sizeof(double);
        private const int _decimalLengthInBytes = sizeof(decimal);

        private const byte _falseAsByte = 0;
        private const byte _trueAsByte = 1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, bool value, out Span<byte> rest) {
            if (destination.Length < _byteLengthInBytes) {
                rest = destination;
                return false;
            }

            destination[0] = value ? _trueAsByte : _falseAsByte;
            rest = destination[_byteLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, byte value, out Span<byte> rest) {
            if (destination.Length < _byteLengthInBytes) {
                rest = destination;
                return false;
            }

            destination[0] = value;
            rest = destination[_byteLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, sbyte value, out Span<byte> rest) {
            if (destination.Length < _sByteLengthInBytes) {
                rest = destination;
                return false;
            }

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            rest = destination[_sByteLengthInBytes..];
            return true;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, short value, out Span<byte> rest) {
            if (destination.Length < _shortLengthInBytes) {
                rest = destination;
                return false;
            }

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            rest = destination[_shortLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, ushort value, out Span<byte> rest) {
            if (destination.Length < _uShortLengthInBytes) {
                rest = destination;
                return false;
            }

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            rest = destination[_uShortLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, int value, out Span<byte> rest) {
            if (destination.Length < _intLengthInBytes) {
                rest = destination;
                return false;
            }

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            rest = destination[_intLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, uint value, out Span<byte> rest) {
            if (destination.Length < _uIntLengthInBytes) {
                rest = destination;
                return false;
            }

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            rest = destination[_uIntLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, long value, out Span<byte> rest) {
            if (destination.Length < _longLengthInBytes) {
                rest = destination;
                return false;
            }

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            rest = destination[_longLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, ulong value, out Span<byte> rest) {
            if (destination.Length < _uLongLengthInBytes) {
                rest = destination;
                return false;
            }

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            rest = destination[_uLongLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, char value, out Span<byte> rest) {
            if (destination.Length < _charLengthInBytes) {
                rest = destination;
                return false;
            }

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            rest = destination[_charLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, float value, out Span<byte> rest) {
            if (destination.Length < _floatLengthInBytes) {
                rest = destination;
                return false;
            }

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            rest = destination[_floatLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, double value, out Span<byte> rest) {
            if (destination.Length < _doubleLengthInBytes) {
                rest = destination;
                return false;
            }

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            rest = destination[_doubleLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in decimal value, out Span<byte> rest) {
            if (destination.Length < _decimalLengthInBytes) {
                rest = destination;
                return false;
            }

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            rest = destination[_decimalLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in ReadOnlySpan<byte> value, out Span<byte> rest) {
            if (destination.Length < value.Length) {
                rest = destination;
                return false;
            }

            value.CopyTo(destination);
            rest = destination[value.Length..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in ReadOnlyMemory<byte> value,
            out Span<byte> rest) =>
            TryWrite(destination, value.Span, out rest);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in ReadOnlySequence<byte> value,
            out Span<byte> rest) {
            if (destination.Length < value.Length) {
                rest = destination;
                return false;
            }

            value.CopyTo(destination);
            rest = destination[(int)value.Length..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in string value, out Span<byte> rest,
            Encoding encoding) {
            int encodedStringLength = encoding.GetByteCount(value);

            if (destination.Length < encodedStringLength + _intLengthInBytes) {
                rest = destination;
                return false;
            }

            rest = destination;

            byte[] byteBuffer = ArrayPool<byte>.Shared.Rent(encodedStringLength);
            Span<byte> byteSpan = byteBuffer[..encodedStringLength];

            try {
                _ = encoding.GetBytes(value, byteSpan);
                return TryWrite(destination, encodedStringLength, out rest) &&
                       TryWrite(rest, byteSpan, out rest);
            } finally {
                ArrayPool<byte>.Shared.Return(byteBuffer);
            }
        }
    }
}