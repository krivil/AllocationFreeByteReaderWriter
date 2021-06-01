namespace AllocationFreeByteReaderWriter.Serializable
{
    using System;

    public class DeserializeException : Exception {
        public Type Type { get; }

        public DeserializeException(Type type) {
            Type = type;
        }

        public DeserializeException(Type type, string? message) : base(message) {
            Type = type;
        }

        public DeserializeException(Type type, string? message, Exception? innerException) : base(message, innerException) {
            Type = type;
        }

    }
}