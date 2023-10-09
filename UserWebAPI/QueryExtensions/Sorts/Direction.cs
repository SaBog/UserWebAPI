using System.Text.Json.Serialization;

namespace UserWebAPI.QueryExtensions.Sorts
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Direction
    {
        None,
        Asc,
        Desc
    }

}
