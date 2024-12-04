using System;

namespace DdgAiProxy
{

    public class Response
    {
        public ResultType Status { get; set; } = ResultType.UnknownStatus;
        public string TextResponse { get; set; }
        #if NETSTANDARD
        public string ModelInfo { get; set; }
        #else
        public string? ModelInfo { get; set; }
        #endif
        public DateTime ResponseTime { get; set; }

        

        public static implicit operator string(Response response)
        {
            return response.TextResponse;
        }
    }
}