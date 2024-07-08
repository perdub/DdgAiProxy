using System.Text.Json.Serialization;

namespace DdgAiProxy;

public class Message{
    [JsonPropertyName("role")]
    public string Role {get;set;}
    [JsonPropertyName("content")]
    public string Content{get;set;}
    public Message(Role role, string message)
    {
        SetRole(role);
        Content = message;
    }
    public void SetRole(Role role){
        Role = role.GetParam();
    }
}