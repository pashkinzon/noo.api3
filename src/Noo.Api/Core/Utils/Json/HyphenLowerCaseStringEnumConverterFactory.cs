using System.Text.Json;
using System.Text.Json.Serialization;

namespace Noo.Api.Core.Utils.Json;

public class HyphenLowerCaseStringEnumConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
        => typeToConvert.IsEnum;

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var converterType = typeof(HyphenLowerCaseStringEnumConverter<>).MakeGenericType(typeToConvert);

        return (JsonConverter)Activator.CreateInstance(converterType)!;
    }

    private class HyphenLowerCaseStringEnumConverter<T> : JsonConverter<T> where T : struct, Enum
    {
        private readonly JsonNamingPolicy _namingPolicy = new HyphenLowerCaseNamingPolicy();

        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string enumString = reader.GetString()!;
            string pascalCase = enumString.Replace("-", "");

            return Enum.Parse<T>(pascalCase, ignoreCase: true);
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            string enumName = value.ToString();
            string hyphenCase = _namingPolicy.ConvertName(enumName);

            writer.WriteStringValue(hyphenCase);
        }
    }
}
