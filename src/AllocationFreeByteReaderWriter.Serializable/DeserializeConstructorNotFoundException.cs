namespace AllocationFreeByteReaderWriter.Serializable
{
    using System;

    public class DeserializeConstructorNotFoundException : Exception {
        public DeserializeConstructorNotFoundException(Type type)
            : base($"Unable to find constructor for new {type.Name}(ReadOnlySpan<byte> bytes, out ReadOnlySpan<byte> rest)") {
            Type = type;
        }

        public DeserializeConstructorNotFoundException(Type type, string? message) : base(message) {
            Type = type;
        }

        public DeserializeConstructorNotFoundException(Type type, string? message, Exception? innerException) : base(message, innerException) {
            Type = type;
        }

        public Type Type { get; }
    }
}