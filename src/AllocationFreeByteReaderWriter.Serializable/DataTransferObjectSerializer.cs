namespace AllocationFreeByteReaderWriter.Serializable {
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class DataTransferObjectSerializer {
        private delegate SerializableDataTransferObject ConstructDtoDelegate(ReadOnlySpan<byte> bytes, out ReadOnlySpan<byte> rest);

        private static readonly ConcurrentDictionary<short, ConstructDtoDelegate> _registeredTypes = new();
        private static readonly Type[] _constructorParameterTypes = { typeof(ReadOnlySpan<byte>), typeof(ReadOnlySpan<byte>).MakeByRefType() };

        public static bool TryRegisterType<T>(short id) where T : SerializableDataTransferObject {
            Type type = typeof(T);
            ConstructDtoDelegate constructorDelegate = CreateConstructorDelegate(type);
            return _registeredTypes.TryAdd(id, constructorDelegate);
        }

        public static void RegisterOrReplaceType<T>(short id) where T : SerializableDataTransferObject {
            Type type = typeof(T);
            ConstructDtoDelegate constructorDelegate = CreateConstructorDelegate(type);
            _registeredTypes[id] = constructorDelegate;
        }

        public static bool TryDeserialize(short id, ReadOnlySpan<byte> bytes, [MaybeNullWhen(false)] out SerializableDataTransferObject value, out ReadOnlySpan<byte> rest) {
            if (_registeredTypes.TryGetValue(id, out ConstructDtoDelegate? ctor)) {
                value = ctor(bytes, out rest);
                return true;
            }

            rest = default;
            value = default;
            return false;
        }

        public static T Deserialize<T>(short id, ReadOnlySpan<byte> bytes, out ReadOnlySpan<byte> rest)
            where T : SerializableDataTransferObject {
            bool success = TryDeserialize(id, bytes, out SerializableDataTransferObject? value, out rest);

            if (!success) {
                throw new DeserializeException(typeof(T));
            }

            return (T)value!;
        }

        private static ConstructDtoDelegate CreateConstructorDelegate(Type type) {
            ConstructorInfo? constructor = type.GetConstructor(_constructorParameterTypes);

            if (constructor == null) throw new DeserializeConstructorNotFoundException(type);

            ParameterInfo[] parametersInfo = constructor.GetParameters();
            Expression[] expArgs = new Expression[parametersInfo.Length];
            List<ParameterExpression> lstParamExpressions = new();

            for (int i = 0; i < expArgs.Length; i++) {
                ParameterInfo parameterInfo = parametersInfo[i];

                Type parameterType = parameterInfo.ParameterType;
                string? parameterName = parameterInfo.Name;

                expArgs[i] = Expression.Parameter(parameterType, parameterName);
                lstParamExpressions.Add((ParameterExpression)expArgs[i]);
            }

            NewExpression callExpression = Expression.New(constructor, expArgs);
            LambdaExpression lambdaExpression = Expression.Lambda<ConstructDtoDelegate>(callExpression, lstParamExpressions);

            return (ConstructDtoDelegate)lambdaExpression.Compile();
        }
    }
}