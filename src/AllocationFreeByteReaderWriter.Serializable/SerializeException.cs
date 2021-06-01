namespace AllocationFreeByteReaderWriter.Serializable
{
    using System;

    public class SerializeException : Exception {
        public SerializeException() {
        }

        public SerializeException(string? message) : base(message) {
        }

        public SerializeException(string? message, Exception? innerException) : base(message, innerException) {
        }

    }
}