using System.Collections.Generic;
using System.Linq;

namespace Application.DTO.Response
{
    public class Result
    {
        internal Result(bool succeeded, IEnumerable<string> errors, IEnumerable<string> messages,int retValue,bool isExist)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
            Messages = messages.ToArray();
            returnId = retValue;
            IsExist = isExist;
        }

        public bool Succeeded { get; init; }


        public string[] Errors { get; init; }

        public string[] Messages { get; init; }
        public int returnId { get; set; }
        public bool IsExist { get; init; }

        public static Result Success(IEnumerable<string> messages, int retValue=0)
        {
            return new Result(true, System.Array.Empty<string>(), messages, retValue,false);
        }

        public static Result Failure(IEnumerable<string> errors,bool isExist= false)
        {
            return new Result(false, errors, System.Array.Empty<string>(),0, isExist);
        }
    }
}
