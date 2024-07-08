using System.Text.Json.Serialization;

public class Response{
    [JsonPropertyName("message")]
    public string Message{get;set;}
    [JsonPropertyName("created")]
    public long UnixTime {get;set;}
    [JsonPropertyName("id")]
    public string Id{get;set;}
    [JsonPropertyName("action")]
    public string Action{get;set;}
    [JsonPropertyName("model")]
    public string Model{get;set;}
}