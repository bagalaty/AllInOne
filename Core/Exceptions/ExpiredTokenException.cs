using Core.Proxy.Http;
using System;

namespace Core.Exceptions
{
    [Serializable]
    public class ExpiredTokenException : Exception
    {
        public AllInOneRequest OriginalRequest { get; set; }
    }
}