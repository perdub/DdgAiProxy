using System.Dynamic;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using DdgAiProxy;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

public class OpenAiCompatibleApiController(CustomClient client) : Controller{
    public class ChatCompletionRequest{
        [JsonPropertyName("model")]
        public string Model {get;set;}
        [JsonPropertyName("messages")]
        public Message[] Messages {get;set;}
    }
        private class respone{
            [JsonPropertyName("created")]
            public string Created;
            [JsonPropertyName("id")]
            public string id;
            [JsonPropertyName("model")]
            public string model;
            [JsonPropertyName(name: "object")]
            public string Obj;
            [JsonPropertyName("choices")]
            public choise[] choises;
        }
        private class choise{
            [JsonPropertyName("message")]
            public Message message;
            [JsonPropertyName("index")]
            public int index = 0;
            [JsonPropertyName("finish_reason")]
            public string stop_reason = "stop";
            [JsonPropertyName("logprobs")]
            public object logprobes = null;
        }
    [Route("/chat/completions")]
    [HttpPost]
    public async Task<IActionResult> Comp([FromBody] ChatCompletionRequest chatCompletionRequest){
        Model model;
        try{
            model = chatCompletionRequest.Model.GetModel();
        }
        catch{
            model = Model.Gpt3_5_turbo;
            HttpContext.Response.Headers.Append("ddg-ai-proxy-notify", "Model incorrect, gpt3.5 will be selected and used.");
        }

        DialogManager dialogManager = new DialogManager(client);
        await dialogManager.Init(model);
        foreach(var inputMessage in chatCompletionRequest.Messages){
            dialogManager.DirectAddMessage(
                inputMessage
            );
        }
        string newData = await dialogManager.Talk();
        

        return Json(new respone{
            Created = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds().ToString(),
            id = "chatcmpl-" + Guid.NewGuid(),
            model = "gemma2-9b-it",
            Obj = "chat.completion",
            choises = new choise[]{
                new choise{
                    message = new Message("assistant",newData)
                    }
                }
        }, new JsonSerializerOptions{IncludeFields=true,WriteIndented=true, DefaultIgnoreCondition = JsonIgnoreCondition.Never});
    }
}