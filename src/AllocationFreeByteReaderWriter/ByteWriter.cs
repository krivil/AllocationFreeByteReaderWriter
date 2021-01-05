namespace AllocationFreeByteReaderWriter {
    using System;
    using System.Buffers;
    using System.Buffers.Binary;
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
        public static bool TryWrite(this in Span<byte> destination, in bool value, out Span<byte> rest) {
            if (destination.Length < _byteLengthInBytes) {
                rest = destination;
                return false;
            }

            destination[0] = value ? _trueAsByte : _falseAsByte;
            rest = destination[_byteLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in byte value, out Span<byte> rest) {
            if (destination.Length < _byteLengthInBytes) {
                rest = destination;
                return false;
            }

            destination[0] = value;
            rest = destination[_byteLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in sbyte value, out Span<byte> rest) {
            if (destination.Length < _sByteLengthInBytes) {
                rest = destination;
                return false;
            }

            rest = destination[_sByteLengthInBytes..];

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            return true;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, short value, out Span<byte> rest) {
            if (destination.Length < _shortLengthInBytes) {
                rest = Span<byte>.Empty;
                return false;
            }

            rest = destination[_shortLengthInBytes..];
            BinaryPrimitives.WriteInt16LittleEndian(destination, value);
            return true;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in short value, out Span<byte> rest) {
            if (destination.Length < _shortLengthInBytes) {
                rest = destination;
                return false;
            }

            rest = destination[_shortLengthInBytes..];

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in ushort value, out Span<byte> rest) {
            if (destination.Length < _uShortLengthInBytes) {
                rest = destination;
                return false;
            }

            rest = destination[_uShortLengthInBytes..];

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in int value, out Span<byte> rest) {
            if (destination.Length < _intLengthInBytes) {
                rest = destination;
                return false;
            }

            rest = destination[_intLengthInBytes..];

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in uint value, out Span<byte> rest) {
            if (destination.Length < _uIntLengthInBytes) {
                rest = destination;
                return false;
            }

            rest = destination[_uIntLengthInBytes..];

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in long value, out Span<byte> rest) {
            if (destination.Length < _longLengthInBytes) {
                rest = destination;
                return false;
            }

            rest = destination[_longLengthInBytes..];

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in ulong value, out Span<byte> rest) {
            if (destination.Length < _uLongLengthInBytes) {
                rest = destination;
                return false;
            }

            rest = destination[_uLongLengthInBytes..];

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in char value, out Span<byte> rest) {
            if (destination.Length < _charLengthInBytes) {
                rest = destination;
                return false;
            }

            rest = destination[_charLengthInBytes..];

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in float value, out Span<byte> rest) {
            if (destination.Length < _floatLengthInBytes) {
                rest = destination;
                return false;
            }

            rest = destination[_floatLengthInBytes..];

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in double value, out Span<byte> rest) {
            if (destination.Length < _doubleLengthInBytes) {
                rest = destination;
                return false;
            }

            rest = destination[_doubleLengthInBytes..];

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in decimal value, out Span<byte> rest) {
            if (destination.Length < _decimalLengthInBytes) {
                rest = destination;
                return false;
            }

            rest = destination[_decimalLengthInBytes..];

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in ReadOnlySpan<byte> value, out Span<byte> rest) {
            if (destination.Length < value.Length) {
                rest = destination;
                return false;
            }

            rest = destination[value.Length..];

            value.CopyTo(destination);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in ReadOnlyMemory<byte> value,
            out Span<byte> rest) {
            if (destination.Length < value.Length) {
                rest = destination;
                return false;
            }

            rest = destination[value.Length..];

            value.Span.CopyTo(destination);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in ReadOnlySequence<byte> value,
            out Span<byte> rest) {
            if (destination.Length < value.Length) {
                rest = destination;
                return false;
            }

            rest = destination[(int)value.Length..];

            value.CopyTo(destination);
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