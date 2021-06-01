namespace AllocationFreeByteReaderWriter.Serializable {
    using System;
    using System.Collections.Generic;

    public static class DelegateSerializer {
        private record TypeDelegateHolder(Type Type, Delegate UntypedGetSizeInBytes, Delegate UntypedSerialize, Delegate UntypedDeserialize);

        private record TypeDelegateHolder<T> : TypeDelegateHolder {
            public TypeDelegateHolder(Type type, GetSizeInBytesForSerializationDelegate<T> getSizeInBytes, SerializeDelegate<T> serialize, DeserializeDelegate<T> deserialize)
            : base(type, getSizeInBytes, serialize, deserialize) {
            }

            public GetSizeInBytesForSerializationDelegate<T> GetSizeInBytes =>
                (GetSizeInBytesForSerializationDelegate<T>)UntypedGetSizeInBytes;
            public SerializeDelegate<T> Serialize => (SerializeDelegate<T>)UntypedSerialize;
            public DeserializeDelegate<T> Deserialize => (DeserializeDelegate<T>)UntypedDeserialize;

            public void Deconstruct(out Type type, out GetSizeInBytesForSerializationDelegate<T> getSize, out SerializeDelegate<T> serialize, out DeserializeDelegate<T> deserialize) {
                type = Type;
                getSize = GetSizeInBytes;
                serialize = Serialize;
                deserialize = Deserialize;
            }
        };

        public delegate bool SerializeDelegate<in T>(T value, Span<byte> bytes, out Span<byte> rest);
        public delegate bool DeserializeDelegate<T>(ReadOnlySpan<byte> bytes, out T value, out ReadOnlySpan<byte> rest);
        public delegate int GetSizeInBytesForSerializationDelegate<in T>(T value);

        private static readonly object _syncLock = new ();
        private static readonly Dictionary<short, TypeDelegateHolder> _registeredTypes = new();
        private static readonly Dictionary<Type, short> _registeredIds = new();


        public static int GetSizeInBytesForSerialization<T>(short id, T value) {
            if (_registeredTypes.TryGetValue(id, out var data)) {
                (Type type, GetSizeInBytesForSerializationDelegate<T> getSize, _, _) = (TypeDelegateHolder<T>)data;

                if (typeof(T) != type) {
                    return -1;
                }

                return getSize(value);
            }

            return -1;
        }

        public static bool TryRegisterType<T>(short id,
            GetSizeInBytesForSerializationDelegate<T> getSizeInBytes,
            SerializeDelegate<T> serialize,
            DeserializeDelegate<T> deserialize) {
            lock (_syncLock) {
                return _registeredIds.TryAdd(typeof(T), id)
                       && _registeredTypes.TryAdd(id, new TypeDelegateHolder<T>(typeof(T), getSizeInBytes, serialize, deserialize));
            }
        }

        public static void RegisterOrReplaceType<T>(short id,
            GetSizeInBytesForSerializationDelegate<T> getSizeInBytes,
            SerializeDelegate<T> serialize,
            DeserializeDelegate<T> deserialize) {

            lock (_syncLock) {
                _registeredIds[typeof(T)] = id;
                _registeredTypes[id] = new TypeDelegateHolder<T>(typeof(T), getSizeInBytes, serialize, deserialize);
            }
        }

        public static bool TryDeserialize<T>(short id, ReadOnlySpan<byte> bytes, out T? value,
            out ReadOnlySpan<byte> rest) {
            bool success;
            TypeDelegateHolder? data;
            lock (_syncLock) {
                success = _registeredTypes.TryGetValue(id, out data);
            }

            if (success) {
                (Type type, _, _, var deserialize) = (TypeDelegateHolder<T>)data!;

                if (typeof(T) == type) return deserialize(bytes, out value, out rest);
            }

            value = default;
            rest = ReadOnlySpan<byte>.Empty;
            return false;
        }

        public static bool TryDeserialize<T>(ReadOnlySpan<byte> bytes, out T? value,
            out ReadOnlySpan<byte> rest) {
            bool success;
            short id;
            lock (_syncLock) {
                success = _registeredIds.TryGetValue(typeof(T), out id);
            }

            if (success) {
                return TryDeserialize(id, bytes, out value, out rest);
            }

            value = default;
            rest = bytes;
            return false;
        }

        public static T Deserialize<T>(ReadOnlySpan<byte> bytes, out ReadOnlySpan<byte> rest) {
            bool success = TryDeserialize(bytes, out T? value, out rest);

            if (!success) {
                throw new DeserializeException(typeof(T));
            }

            return value!;
        }

        public static bool TrySerialize<T>(short id, T value, Span<byte> bytes, out Span<byte> rest) {
            bool success;
            TypeDelegateHolder? data;
            lock (_syncLock) {
                success = _registeredTypes.TryGetValue(id, out data);
            }

            if (success) {
                (Type type, _, var serialize, _) = (TypeDelegateHolder<T>)data!;

                if (typeof(T) == type) return serialize(value, bytes, out rest);
            }

            rest = Span<byte>.Empty;
            return false;
        }

        public static bool TrySerialize<T>(T value, Span<byte> bytes, out Span<byte> rest) {
            bool success;
            short id;
            lock (_syncLock) {
                success = _registeredIds.TryGetValue(typeof(T), out id);
            }

            if (success) {
                return TrySerialize(id, value, bytes, out rest);
            }

            rest = bytes;
            return false;
        }

        public static void Serialize<T>(T value, Span<byte> bytes, out Span<byte> rest) {
            bool success = TrySerialize(value, bytes, out rest);

            if (!success) {
                throw new SerializeException();
            }
        }
    }
}
