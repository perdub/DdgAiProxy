using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
namespace DdgAiProxy
{
    public class CustomClient : HttpClient
    {
        public CustomClient() : this(false)
        {

        }
        public CustomClient(bool useProxy = false, Uri proxyAddress = null, bool useCredentials = true, string proxyUserName = null, string proxyPassword = null)
         : base(buildHandler(useProxy, proxyAddress, useCredentials, proxyUserName, proxyPassword))
        {
#if NET5_0_OR_GREATER
            DefaultRequestVersion = new Version(2,0);
#endif
            add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/131.0.0.0 Safari/537.36");
            
            
            add("Accept", "text/event-stream");
            add("Priority", "u=1, i");
            add("Referer", "https://duckduckgo.com/");
            add("Origin", "https://duckduckgo.com");
        }
        private static HttpClientHandler buildHandler(bool useProxy = false, Uri proxyAddress = null, bool useCredentials = true, string proxyUserName = null, string proxyPassword = null)
        {
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            CookieContainer cookieContainer = new CookieContainer();
            httpClientHandler.CookieContainer = cookieContainer;
            if (useProxy)
            {
                var proxy = new WebProxy
                {
                    Address = proxyAddress,
                    UseDefaultCredentials = false,
                    BypassProxyOnLocal = false,
                };
                if (useCredentials)
                {
                    proxy.Credentials = new NetworkCredential(
                        proxyUserName,
                        proxyPassword
                    );
                }
                httpClientHandler.Proxy = proxy;
                //allow all cert(are we really need this?)
                httpClientHandler.ServerCertificateCustomValidationCallback =
#if NETSTANDARD
(a, b, c, d) => { return true; };
#else
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
#endif
            }
            return httpClientHandler;
        }
        private void add(string name, string value)
        {
            DefaultRequestHeaders.Add(name, value);
        }
#if NETSTANDARD
        public HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
#else
        public override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
#endif
        {
            var resp = this.SendAsync(request, cancellationToken);
            resp.Wait();
            return resp.Result;
        }
        public new Task<HttpResponseMessage> SendAsync(HttpRequestMessage request){
            return this.SendAsync(request, CancellationToken.None);
        }
        public override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("Cookie", "dcs=1; dcm=3");
            return base.SendAsync(request, cancellationToken);
        }
        public new async Task<HttpResponseMessage> PostAsync(
            //dont ask me
#if NETSTANDARD
#else
            [StringSyntax("Uri")]
#endif

            string uri,
            HttpContent httpContent,
            string vqdCode
        )
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
            httpRequestMessage.Content = httpContent;
            httpRequestMessage.Headers.Add("X-Vqd-4", vqdCode);
            return await SendAsync(httpRequestMessage, CancellationToken.None);
        }
        public new async Task<HttpResponseMessage> GetAsync(
            //dont ask me
#if NETSTANDARD
#else
            [StringSyntax("Uri")]
#endif
            
            string uri,
            CancellationToken? cancellationToken = null
        )
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
            return await SendAsync(httpRequestMessage, cancellationToken ?? CancellationToken.None);
        }
    }
}