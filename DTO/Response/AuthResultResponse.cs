namespace DTO.Response
{
    public class AuthResultResponse
    {
        public bool IsSuccess { get; init; }
        public string AccessToken { get; init; }
        public string ErrorMessage { get; init; }
    }
}
