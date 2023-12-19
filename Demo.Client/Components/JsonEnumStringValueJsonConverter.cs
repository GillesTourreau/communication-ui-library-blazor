//-----------------------------------------------------------------------
// <copyright file="JsonEnumStringValueJsonConverter.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor
{
    using System.Reflection;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public sealed class JsonEnumStringValueJsonConverter<TEnum> : JsonConverter<TEnum>
        where TEnum : struct, Enum
    {
        private static readonly IDictionary<TEnum, string> enumToString;

        private static readonly IDictionary<string, TEnum> stringToEnum;

        static JsonEnumStringValueJsonConverter()
        {
            var fields = typeof(TEnum).GetFields();

            enumToString = new Dictionary<TEnum, string>(fields.Length);
            stringToEnum = new Dictionary<string, TEnum>(fields.Length);

            foreach (var enumName in Enum.GetNames<TEnum>())
            {
                var field = typeof(TEnum).GetField(enumName);
                var enumValue = Enum.Parse<TEnum>(enumName);

                var attribute = field.GetCustomAttribute<JsonEnumStringValueAttribute>();

                if (attribute is null)
                {
                    enumToString.Add(enumValue, field.Name);
                    stringToEnum.Add(field.Name, enumValue);
                }
                else
                {
                    enumToString.Add(enumValue, attribute.Value);
                    stringToEnum.Add(attribute.Value, enumValue);
                }
            }
        }

        public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var stringValue = reader.GetString();

            if (stringValue is null)
            {
                return default;
            }

            if (!stringToEnum.TryGetValue(stringValue, out var enumValue))
            {
                return default;
            }

            return enumValue;
        }

        public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
        {
            if (!enumToString.TryGetValue(value, out var stringValue))
            {
                throw new JsonException($"The '{value}' is not a valid value for the the '{typeof(TEnum).Name}'");
            }

            writer.WriteStringValue(stringValue);
        }
    }
}
