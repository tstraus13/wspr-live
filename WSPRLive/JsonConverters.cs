using System.Text.Json;
using System.Text.Json.Serialization;

namespace WSPRLive;

public static class JsonConverters
{
    public class LongConverter : JsonConverter<long>
    {
        public override long Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    var value = reader.GetString();
                    var parsedValue = string.IsNullOrEmpty(value) ? "0" : value;

                    if (long.TryParse(parsedValue, out long result))
                        return result;

                    return 0;
                case JsonTokenType.Number:
                    return reader.GetInt64();
                default:
                    return 0;
            }
        }

        public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }

    public class DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    var value = reader.GetString();
                    
                    if (string.IsNullOrEmpty(value))
                        return DateTime.MinValue;

                    if (DateTime.TryParse(value, out DateTime result))
                        return result;

                    return DateTime.MinValue;
                default:
                    return DateTime.MinValue;
            }
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}