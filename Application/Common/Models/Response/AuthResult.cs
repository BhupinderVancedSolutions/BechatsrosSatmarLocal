﻿namespace Application.Models.Response
{
    public class AuthResult
    {
        public bool IsSuccess { get; init; }
        public object Result { get; init; }
        public string ErrorMessage { get; init; }
        public string SuccessMessage { get; init; }
    }
}
