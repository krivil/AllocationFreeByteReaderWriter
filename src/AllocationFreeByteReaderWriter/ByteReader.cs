namespace AllocationFreeByteReaderWriter {
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    public static class ByteReader {
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRead(this in ReadOnlySpan<byte> source, out bool value, out ReadOnlySpan<byte> rest) {
            if (source.Length < _byteLengthInBytes) {
                value = default;
                rest = source;
                return false;
            }

            value = source[0] != _falseAsByte;
            rest = source[_byteLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRead(this in ReadOnlySpan<byte> source, out byte value, out ReadOnlySpan<byte> rest) {
            if (source.Length < _byteLengthInBytes) {
                value = default;
                rest = source;
                return false;
            }

            value = source[0];
            rest = source[_byteLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRead(this in ReadOnlySpan<byte> source, out sbyte value, out ReadOnlySpan<byte> rest) {
            if (source.Length < _sByteLengthInBytes) {
                value = default;
                rest = source;
                return false;
            }

            value = Unsafe.ReadUnaligned<sbyte>(ref MemoryMarshal.GetReference(source));
            rest = source[_sByteLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRead(this in ReadOnlySpan<byte> source, out short value, out ReadOnlySpan<byte> rest) {
            if (source.Length < _shortLengthInBytes) {
                value = default;
                rest = source;
                return false;
            }

            ReadOnlySpan<byte> valueSpan = source[.._shortLengthInBytes];
            value = Unsafe.ReadUnaligned<short>(ref MemoryMarshal.GetReference(valueSpan));
            rest = source[_shortLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRead(this in ReadOnlySpan<byte> source, out ushort value, out ReadOnlySpan<byte> rest) {
            if (source.Length < _uShortLengthInBytes) {
                value = default;
                rest = source;
                return false;
            }

            ReadOnlySpan<byte> valueSpan = source[.._uShortLengthInBytes];
            value = Unsafe.ReadUnaligned<ushort>(ref MemoryMarshal.GetReference(valueSpan));
            rest = source[_uShortLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRead(this in ReadOnlySpan<byte> source, out int value, out ReadOnlySpan<byte> rest) {
            if (source.Length < _intLengthInBytes) {
                value = default;
                rest = source;
                return false;
            }

            ReadOnlySpan<byte> valueSpan = source[.._intLengthInBytes];
            value = Unsafe.ReadUnaligned<int>(ref MemoryMarshal.GetReference(valueSpan));
            rest = source[_intLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRead(this in ReadOnlySpan<byte> source, out uint value, out ReadOnlySpan<byte> rest) {
            if (source.Length < _uIntLengthInBytes) {
                value = default;
                rest = source;
                return false;
            }

            ReadOnlySpan<byte> valueSpan = source[.._uIntLengthInBytes];
            value = Unsafe.ReadUnaligned<uint>(ref MemoryMarshal.GetReference(valueSpan));
            rest = source[_uIntLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRead(this in ReadOnlySpan<byte> source, out long value, out ReadOnlySpan<byte> rest) {
            if (source.Length < _longLengthInBytes) {
                value = default;
                rest = source;
                return false;
            }

            ReadOnlySpan<byte> valueSpan = source[.._longLengthInBytes];
            value = Unsafe.ReadUnaligned<long>(ref MemoryMarshal.GetReference(valueSpan));
            rest = source[_longLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRead(this in ReadOnlySpan<byte> source, out ulong value, out ReadOnlySpan<byte> rest) {
            if (source.Length < _uLongLengthInBytes) {
                value = default;
                rest = source;
                return false;
            }

            ReadOnlySpan<byte> valueSpan = source[.._uLongLengthInBytes];
            value = Unsafe.ReadUnaligned<ulong>(ref MemoryMarshal.GetReference(valueSpan));
            rest = source[_uLongLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRead(this in ReadOnlySpan<byte> source, out char value, out ReadOnlySpan<byte> rest) {
            if (source.Length < _charLengthInBytes) {
                value = default;
                rest = source;
                return false;
            }

            ReadOnlySpan<byte> valueSpan = source[.._charLengthInBytes];
            value = Unsafe.ReadUnaligned<char>(ref MemoryMarshal.GetReference(valueSpan));
            rest = source[_charLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRead(this in ReadOnlySpan<byte> source, out float value, out ReadOnlySpan<byte> rest) {
            if (source.Length < _floatLengthInBytes) {
                value = default;
                rest = source;
                return false;
            }

            ReadOnlySpan<byte> valueSpan = source[.._floatLengthInBytes];
            value = Unsafe.ReadUnaligned<float>(ref MemoryMarshal.GetReference(valueSpan));
            rest = source[_floatLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRead(this in ReadOnlySpan<byte> source, out double value, out ReadOnlySpan<byte> rest) {
            if (source.Length < _doubleLengthInBytes) {
                value = default;
                rest = source;
                return false;
            }

            ReadOnlySpan<byte> valueSpan = source[.._doubleLengthInBytes];
            value = Unsafe.ReadUnaligned<double>(ref MemoryMarshal.GetReference(valueSpan));
            rest = source[_doubleLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRead(this in ReadOnlySpan<byte> source, out decimal value, out ReadOnlySpan<byte> rest) {
            if (source.Length < _decimalLengthInBytes) {
                value = default;
                rest = source;
                return false;
            }

            ReadOnlySpan<byte> valueSpan = source[.._decimalLengthInBytes];
            value = Unsafe.ReadUnaligned<decimal>(ref MemoryMarshal.GetReference(valueSpan));
            rest = source[_decimalLengthInBytes..];
            return true;
        }

        /// <summary>
        ///     This method copies from one span to the other. It is slow and you shouldn't use it
        /// </summary>
        /// <param name="source">Span from which to read.</param>
        /// <param name="value">Span which should be filled from source span.</param>
        /// <param name="rest">What's left from source after copying.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRead(this in ReadOnlySpan<byte> source, Span<byte> value, out ReadOnlySpan<byte> rest) {
            if (source.Length < value.Length) {
                rest = source;
                return false;
            }

            source[..value.Length].CopyTo(value);
            rest = source[value.Length..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRead(this in ReadOnlySpan<byte> source, out ReadOnlySpan<byte> value, int length,
            out ReadOnlySpan<byte> rest) {
            if (source.Length < length) {
                rest = source;
                value = Span<byte>.Empty;
                return false;
            }

            value = source[..length];
            rest = source[value.Length..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRead(this in ReadOnlySpan<byte> source, out string? value, out ReadOnlySpan<byte> rest,
            Encoding encoding) {
            bool success = TryRead(source, out int length, out rest);
            if (!success || rest.Length < length) {
                value = default;
                return false;
            }

            value = encoding.GetString(rest[..length]);
            rest = rest[value.Length..];
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