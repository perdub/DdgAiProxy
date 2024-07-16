using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace DdgAiProxy
{

    public static class HttpClientFactory
    {
        private static CustomClient _cached;
        delegate void Add(string name, string value);
        public static CustomClient BuildOrLoadClient()
        {
            if (_cached != null)
                return _cached;
            _cached = new CustomClient();
            return _cached;
        }
    }

}