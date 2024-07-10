using System.Text.Json;
using System.Text.Json.Serialization;

namespace DdgAiProxy;

public static class PayloadBuilder{
    public static Payload BuildNew(Model model = Model.Gpt3_5_turbo, string prompt = "say hi"){
        Payload payload = new Payload();
        payload.ModelName = model.GetName();
        payload.Messages = new Message[]{
            new Message(Role.User, prompt)
        };
        return payload;
    }
    public static Payload BuildEmpty(Model model = Model.Gpt3_5_turbo){
        Payload payload = new Payload();
        payload.ModelName = model.GetName();
        return payload;
    }
    public static string ToJson(this Payload payload){
        return JsonSerializer.Serialize<Payload>(payload, new JsonSerializerOptions{
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        });
    }
    public static void AddMessage(this Payload payload, string userMessage, Role role = Role.User){
        payload.AddMessage(new Message(role, userMessage));
    }
    public static void AddMessage(this Payload payload, Message message){
        Message[] messages = new Message[payload.Messages.Length+1];
        Array.Copy(payload.Messages, messages, payload.Messages.Length);
        messages[payload.Messages.Length] = message;
        payload.Messages = messages;
    }
}