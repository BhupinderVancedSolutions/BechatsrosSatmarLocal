namespace Application.Common.DTO.Response
{
    public class ApplicationUserResponse
    {
        public int UserId { get; init; }
        public string FirstName { get; init; } 
        public string LastName { get; init; }
        public string Email { get; init; }
        public bool IsActive { get; init; }
        public int RoleId { get; init; }
        public string ProfileImagePath { get; set; }
        public bool IsSuperAdmin { get; set; }
        public string RoleName { get; init; }
        public byte[] Password { get; set; }
    }
}
