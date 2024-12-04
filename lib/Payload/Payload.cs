using System.Text.Json.Serialization;

namespace DdgAiProxy
{

    public class Payload
    {
        [JsonPropertyName("model")]
        public string ModelName { get; set; }
        [JsonPropertyName("messages")]
        public Message[] Messages { get; set; } = new Message[0];

        [JsonIgnore]
        public int MessagesCount{
            get{
                return Messages.Length;
            }
        }
    }
}