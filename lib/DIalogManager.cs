using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using DdgAiProxy;

namespace DdgAiProxy;

public class DialogManager
{
    private Model model = Model.Gpt3_5_turbo;
    private Payload payload;
    private CustomClient client;

    private string vqdCode = string.Empty;
    public DialogManager(CustomClient customClient)
    {
        client = customClient;
    }
    public async Task Init(Model model = Model.Gpt3_5_turbo)
    {
        payload = PayloadBuilder.BuildEmpty(model);
        this.model = model;
        await getCode();
    }
    public async Task<string> SendMessage(string message)
    {
        if(payload is null){
            payload = PayloadBuilder.BuildNew(model, message);
        }
        else{
            payload.AddMessage(message);
        }

        return await Talk();
    }

    public async Task<string> Talk(){
        if(string.IsNullOrEmpty(vqdCode)){
            Debug.WriteLine("Warning: context are not init yet. Default model will be use.");
            await Init(Model.Gpt3_5_turbo);
        }
        string json = payload.ToJson();
        var content = new StringContent(json, Encoding.UTF8, new MediaTypeHeaderValue("application/json"));

        var respone = await client.PostAsync(
            "https://duckduckgo.com/duckchat/v1/chat",
            content,
            vqdCode);
        Debug.Print(respone.StatusCode.ToString());
        vqdCode = respone.Headers.GetValues("x-vqd-4").First();
        var stream = respone.Content.ReadAsStream();
        var sr = new StreamReader(stream);
        StringBuilder responseBuilder = new StringBuilder();
        while(!sr.EndOfStream){
            string data = sr.ReadLine();
            if(!string.IsNullOrEmpty(data) && data!="data: [DONE]"){
                data = data.Replace("data:", "").Trim();
                var obj = JsonSerializer.Deserialize<Response>(data);
                responseBuilder.Append(obj.Message);
            }
        }
        string final = responseBuilder.ToString();
        payload.AddMessage(final, Role.AI);
        return final;
    }

    private async Task getCode(){
        HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "https://duckduckgo.com/duckchat/v1/status");
        httpRequestMessage.Headers.Add("x-vqd-accept", "1");
        var respSt = await client.SendAsync(httpRequestMessage);

        IEnumerable<string>? headers;
        bool result = respSt.Headers.TryGetValues("x-vqd-4", out headers);
        if(result){
            vqdCode = headers.First();
        }
        else{
            throw new Exception(
                $"Fall to get code: {respSt.StatusCode}, {await respSt.Content.ReadAsStringAsync()}"
            );
        }
    }

    public void DirectAddMessage(Message message){

        payload.AddMessage(message);
    }

}