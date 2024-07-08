using System.Dynamic;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using DdgAiProxy;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

public class OpenAiCompatibleApiController : Controller{
    private class Usage
    {
        public int prompt_tokens { get; set; } = 100;
        public double prompt_time { get; set; } = 0.004;
        public int completion_tokens { get; set; } = 200;
        public double completion_time { get; set; } = 0.1;
        public int total_tokens { get; set; } = 588;
        public double total_time { get; set; } = 0.55;
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
    public async Task<IActionResult> Comp(){



        return Json(new respone{
            Created = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds().ToString(),
            id = "chatcmpl-" + Guid.NewGuid(),
            model = "gemma2-9b-it",
            Obj = "chat.completion",
            choises = new choise[]{
                new choise{
                    message = new Message("assistant","блять")
                    }
                }
        }, new JsonSerializerOptions{IncludeFields=true,WriteIndented=true, DefaultIgnoreCondition = JsonIgnoreCondition.Never});
    }
}