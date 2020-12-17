using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace AllocationFreeByteReaderWriter
{
    public static class ByteWriter
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
        private const byte TrueAsByte = 1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in bool value, out Span<byte> rest)
        {
            if (destination.Length < ByteLengthInBytes)
            {
                rest = destination;
                return false;
            }

            destination[0] = value ? TrueAsByte : FalseAsByte;
            rest = destination[ByteLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in byte value, out Span<byte> rest)
        {
            if (destination.Length < ByteLengthInBytes)
            {
                rest = destination;
                return false;
            }

            destination[0] = value;
            rest = destination[ByteLengthInBytes..];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in sbyte value, out Span<byte> rest)
        {
            if (destination.Length < SByteLengthInBytes)
            {
                rest = destination;
                return false;
            }

            rest = destination[SByteLengthInBytes..];

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            return true;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this Span<byte> destination, short value, out Span<byte> rest)
        {
            if (destination.Length < ShortLengthInBytes)
            {
                rest = Span<byte>.Empty;
                return false;
            }
            rest = destination[ShortLengthInBytes..];
            BinaryPrimitives.WriteInt16LittleEndian(destination, value);
            return true;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in short value, out Span<byte> rest)
        {
            if (destination.Length < ShortLengthInBytes)
            {
                rest = destination;
                return false;
            }

            rest = destination[ShortLengthInBytes..];

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in ushort value, out Span<byte> rest)
        {
            if (destination.Length < UShortLengthInBytes)
            {
                rest = destination;
                return false;
            }

            rest = destination[UShortLengthInBytes..];

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in int value, out Span<byte> rest)
        {
            if (destination.Length < IntLengthInBytes)
            {
                rest = destination;
                return false;
            }

            rest = destination[IntLengthInBytes..];

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in uint value, out Span<byte> rest)
        {
            if (destination.Length < UIntLengthInBytes)
            {
                rest = destination;
                return false;
            }

            rest = destination[UIntLengthInBytes..];

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in long value, out Span<byte> rest)
        {
            if (destination.Length < LongLengthInBytes)
            {
                rest = destination;
                return false;
            }

            rest = destination[LongLengthInBytes..];

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in ulong value, out Span<byte> rest)
        {
            if (destination.Length < ULongLengthInBytes)
            {
                rest = destination;
                return false;
            }

            rest = destination[ULongLengthInBytes..];

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in char value, out Span<byte> rest)
        {
            if (destination.Length < CharLengthInBytes)
            {
                rest = destination;
                return false;
            }

            rest = destination[CharLengthInBytes..];

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in float value, out Span<byte> rest)
        {
            if (destination.Length < FloatLengthInBytes)
            {
                rest = destination;
                return false;
            }

            rest = destination[FloatLengthInBytes..];

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in double value, out Span<byte> rest)
        {
            if (destination.Length < DoubleLengthInBytes)
            {
                rest = destination;
                return false;
            }

            rest = destination[DoubleLengthInBytes..];

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in decimal value, out Span<byte> rest)
        {
            if (destination.Length < DecimalLengthInBytes)
            {
                rest = destination;
                return false;
            }

            rest = destination[DecimalLengthInBytes..];

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in ReadOnlySpan<byte> value, out Span<byte> rest)
        {
            if (destination.Length < value.Length)
            {
                rest = destination;
                return false;
            }

            rest = destination[value.Length..];

            value.CopyTo(destination);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in ReadOnlyMemory<byte> value, out Span<byte> rest)
        {
            if (destination.Length < value.Length)
            {
                rest = destination;
                return false;
            }

            rest = destination[value.Length..];

            value.Span.CopyTo(destination);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in ReadOnlySequence<byte> value, out Span<byte> rest)
        {
            if (destination.Length < value.Length)
            {
                rest = destination;
                return false;
            }

            rest = destination[(int)value.Length..];

            value.CopyTo(destination);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryWrite(this in Span<byte> destination, in string value, out Span<byte> rest, Encoding encoding)
        {
            int encodedStringLength = encoding.GetByteCount(value);

            if (destination.Length < encodedStringLength + IntLengthInBytes)
            {
                rest = destination;
                return false;
            }

            bool success;
            rest = destination;

            byte[] byteBuffer = ArrayPool<byte>.Shared.Rent(encodedStringLength);
            Span<byte> byteSpan = byteBuffer[..encodedStringLength];

            try
            {
                _ = encoding.GetBytes(value, byteSpan);
                success = TryWrite(destination, encodedStringLength, out rest) &&
                          TryWrite(rest, byteSpan, out rest);
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(byteBuffer);
            }
            return success;
        }

    }
}