using System.Text.Json;
using System.Text.Json.Serialization;

namespace web_api.config
{
    public class JsonElementConverter : JsonConverter<JsonElement>
    {
        public override JsonElement Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return JsonSerializer.Deserialize<JsonElement>(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, JsonElement value, JsonSerializerOptions options)
        {
            value.WriteTo(writer); // Write the JsonElement back as raw JSON
        }
    }

}
