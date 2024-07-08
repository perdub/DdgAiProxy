using System.Text.Json.Serialization;

namespace DdgAiProxy;

public class Payload
{
    [JsonPropertyName("model")]
    public string ModelName {get;set;}
    [JsonPropertyName("messages")]
    public Message[] Messages {get;set;}
}