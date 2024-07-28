using IndFusion.Components.JsonConverters;
using System.Text.Json.Serialization;

namespace IndFusion.Components.Shared.Enums
{
    [JsonConverter(typeof(EventTypeJsonConverter))]
    public enum EventType
    {
        Default,
        Focusout,
        Toggle,
        Mouseover,
        Mouseout,
        Mouseenter,
        Mouseleave,
        TransitionEnd,
        Keyup,
        Resize,
        Click,
        Hide,
        Show,
        Sync

    }
}