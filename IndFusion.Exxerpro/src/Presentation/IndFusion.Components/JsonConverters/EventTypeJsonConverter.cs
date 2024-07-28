using IndFusion.Components.Shared.Enums;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace IndFusion.Components.JsonConverters
{
    public class EventTypeJsonConverter : JsonConverter<EventType>
    {
        public override EventType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (Enum.TryParse(typeof(EventType), reader.GetString(), true, out var value))
            {
                if (value != null)
                    return (EventType)value;
            }
            return EventType.Default;
        }

        public override void Write(Utf8JsonWriter writer, EventType eventType, JsonSerializerOptions options) =>
            writer.WriteStringValue(eventType.ToString());
    }
}