using System.Diagnostics.CodeAnalysis;
using System.Net;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;

namespace DdgAiProxy;

public static class HttpClientFactory{
    private static CustomClient _cached;
    delegate void Add(string name, string value);
    public static CustomClient BuildOrLoadClient(){
        if(_cached is not null)
            return _cached;

        

        _cached = new CustomClient();
        return _cached;
    }
}

public class CustomClient : HttpClient
{
    public CustomClient() : base(buildHandler())
    {
        DefaultRequestVersion = HttpVersion.Version20;
        add("Host", "duckduckgo.com");
        add("User-Agent", "curl/8.7.1");
        add("Sec-Ch-Ua", "\"Not/A)Brand\";v=\"8\", \"Chromium\";v=\"126\", \"Google Chrome\";v=\"126\"");
        add("Sec-Ch-Ua-Mobile", "?0");
        add("Sec-Fetch-Dest", "empty");
        add("Sec-Ch-Ua-Platform", "Windows");
        add("Sec-Fetch-Mode", "cors");
        add("Sec-Fetch-Site", "same-origin");
        add("Accept-Language", "en-US,en;q=0.5");
        add("Accept", "text/event-stream");
        add("Accept-Encoding","gzip, deflate, br");
        add("priority", "u=1, i");
    }
    private static HttpClientHandler buildHandler(){
        CookieContainer cookieContainer = new CookieContainer();
        return new HttpClientHandler{
            CookieContainer = cookieContainer
        };
    }
    private void add(string name, string value){
        DefaultRequestHeaders.Add(name, value);
    }
    public override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Add("Cookies", "dcm=1");
        return base.Send(request, cancellationToken);
    }
    public override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Add("Cookies", "dcm=1");
        return base.SendAsync(request, cancellationToken);
    }
    public new async Task<HttpResponseMessage> PostAsync(
        [StringSyntax("Uri")] string uri,
        HttpContent httpContent,
        string vqdCode
    ){
        HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
        httpRequestMessage.Content = httpContent;
        httpRequestMessage.Headers.Add("x-vqd-4", vqdCode);
        return await SendAsync(httpRequestMessage, CancellationToken.None);
    }
    public new async Task<HttpResponseMessage> GetAsync(
        [StringSyntax("Uri")] string uri,
        CancellationToken? cancellationToken = null
    ){
        HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        return await SendAsync(httpRequestMessage, cancellationToken??CancellationToken.None);
    }
}