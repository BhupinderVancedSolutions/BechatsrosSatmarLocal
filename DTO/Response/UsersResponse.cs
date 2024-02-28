using System.Collections.Generic;

namespace DTO.Response
{
    public class UsersResponse: CommonResponse
    {
        public List<UserResponse> Items { get; init; }
    }

    public class UserResponse
    {
        public string UserId { get; init;}
        public string UserName { get; init;}
        public string EmailId { get; init;}
        public string DisplayName { get; init;}
        public string Password { get; init;}
        public IEnumerable<string> Roles { get; init;}
    }
}
