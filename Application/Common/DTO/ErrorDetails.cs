using System;

namespace Application.Common.DTO
{
    public class ErrorDetails
    {
        public int ErrorId { get; init; }
        public int StatusCode { get; init; }
        public string ErrorDescription { get; init; }
        public string InnerException { get; init; }
        public string Source { get; init; }
        public DateTime ErrorTime { get; init; }
    }
}
