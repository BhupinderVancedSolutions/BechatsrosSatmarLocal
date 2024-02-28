using System;
using System.Collections.Generic;

namespace Common.Exceptions
{
    public class UserDefinedException : Exception
    {
        public UserDefinedException(string errorMessage)
           : base(errorMessage)
        {
            Errors = new Dictionary<string, string[]>
            {
                { "Message", new string[]{ errorMessage } },
            };
        }

        public IDictionary<string, string[]> Errors { get; }
    }
}
