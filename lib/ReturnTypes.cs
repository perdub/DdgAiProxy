using System;

namespace DdgAiProxy
{

    public enum ResultType
    {
        Ok,
        ConservationLimit,
        UpstreamError,
        InputLimit,
        UnknownStatus
    }
}