using System.Text.Json.Serialization;

namespace DdgAiProxy
{
    public class ApiResponse
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
        [JsonPropertyName("created")]
        public long UnixTime { get; set; }
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("action")]
        public string Action { get; set; }
        [JsonPropertyName("model")]
        public string Model { get; set; }
        [JsonPropertyName("type")]
        public string ErrorType { get; set; }
        [JsonPropertyName("status")]
        public int ErrorStatus { get; set; }
    }
}