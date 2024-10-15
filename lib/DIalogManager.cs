using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DdgAiProxy;

namespace DdgAiProxy
{

    public class DialogManager
    {
        private Model model = Model.Gpt4oMini;
        protected Payload payload;
        protected CustomClient client;

        protected string vqdCode = string.Empty;

        public virtual bool IsReady
        {
            get
            {
                return !string.IsNullOrEmpty(vqdCode);
            }
        }

        public virtual Model SurrentModel{
            get{
                return model;
            }
        }
        public DialogManager(CustomClient customClient)
        {
            client = customClient;
        }
        public virtual async Task Init(Model model = Model.Gpt4oMini)
        {
            payload = PayloadBuilder.BuildEmpty(model);
            this.model = model;
            await getCode();
        }
        public virtual async Task<Response> SendMessage(string message)
        {
            if (payload is null)
            {
                payload = PayloadBuilder.BuildNew(model, message);
            }
            else
            {
                payload.AddMessage(message, Role.User);
            }

            return await Talk();
        }

        public virtual async Task<Response> Talk()
        {
            if (string.IsNullOrEmpty(vqdCode))
            {
                throw new DialogNotInitException("Dialog are not init, you should call Init() before sending messages.");
            }
            string json = payload.ToJson();
            var content = new StringContent(json, Encoding.UTF8, (new MediaTypeHeaderValue("application/json")).ToString());

            var respone = await client.PostAsync(
                "https://duckduckgo.com/duckchat/v1/chat",
                content,
                vqdCode);

            Response response = new Response();
            response.ResponseTime = DateTime.Now;

            if (respone.StatusCode != System.Net.HttpStatusCode.OK)
            {
                /*
                Debug.Print(respone.StatusCode.ToString());
                throw new FalledRequestException($"Fall to send chat request. Status code: {respone.StatusCode}");*/
                switch(respone.StatusCode){
                    case System.Net.HttpStatusCode.Gone:
                        Debug.Print("410 code returned. Context on server side are reach a livetime end.");
                        response.Status = ResultType.Outdated;
                        return response;
                }
            }

            vqdCode = respone.Headers.GetValues("x-vqd-4").First();
            var stream = await respone.Content.ReadAsStreamAsync();
            var sr = new StreamReader(stream);
            StringBuilder responseBuilder = new StringBuilder();
            while (!sr.EndOfStream)
            {
                string data = sr.ReadLine();
                if (!string.IsNullOrEmpty(data) && data != "data: [DONE]")
                {
                    if(data == "data: [DONE][LIMIT_CONVERSATION]"){
                        response.Status = ResultType.ConservationLimit;
                    }
                    data = data.Replace("data:", "").Trim();
                    var obj = JsonSerializer.Deserialize<ApiResponse>(data);
                    if(obj.Action == "success"){
                        responseBuilder.Append(obj.Message);
                        if (response.ModelInfo is null)
                        {
                            response.ModelInfo = obj.Model;
                        }
                    }
                    else if(obj.Action == "error"){
                        switch(obj.ErrorStatus){
                            case 504:
                                response.Status = ResultType.UpstreamError;
                                break;
                            case 429:
                                response.Status = ResultType.InputLimit;
                                break;
                        }
                    }
                }
            }
            string final = responseBuilder.ToString();
            payload.AddMessage(final, Role.AI);

            response.TextResponse = final;
            if(response.Status == ResultType.UnknownStatus)
                response.Status = ResultType.Ok;
            return response;
        }

        private async Task getCode()
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "https://duckduckgo.com/duckchat/v1/status");
            httpRequestMessage.Headers.Add("x-vqd-accept", "1");
            var respSt = await client.SendAsync(httpRequestMessage);

            IEnumerable<string> headers;
            bool result = respSt.Headers.TryGetValues("x-vqd-4", out headers);
            if (result)
            {
                vqdCode = headers.First();
            }
            else
            {
                throw new Exception(
                    $"Fall to get code: {respSt.StatusCode}, {await respSt.Content.ReadAsStringAsync()}"
                );
            }
        }

        public virtual void DirectAddMessage(Message message)
        {
            payload.AddMessage(message);
        }

    }
#if NETSTANDARD
    internal static class NetStandartHelper
    {
        public static async Task<Stream> ReadAsStreamAsync(this HttpContent httpContent)
        {
            var bytearr = await httpContent.ReadAsByteArrayAsync();
            MemoryStream memoryStream = new MemoryStream();
            memoryStream.Write(bytearr, 0, bytearr.Length);
            return memoryStream;
        }
    }
#endif
}