using System.Collections.Generic;
using System.Linq;

namespace DTO.Response
{
    public class ResultResponse
    {
        internal ResultResponse(bool succeeded, IEnumerable<string> errors, IEnumerable<string> messages)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
            Messages = messages.ToArray();
        }

        public bool Succeeded { get; init; }

        public string[] Errors { get; init; }

        public string[] Messages { get; init; }

        public static ResultResponse Success(IEnumerable<string> messages)
        {
            return new ResultResponse(true, System.Array.Empty<string>(), messages);
        }

        public static ResultResponse Failure(IEnumerable<string> errors)
        {
            return new ResultResponse(false, errors, System.Array.Empty<string>());
        }
    }
}
