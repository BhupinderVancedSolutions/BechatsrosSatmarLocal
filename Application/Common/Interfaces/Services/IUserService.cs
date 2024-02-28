using Application.Common.Models.Request;
using Application.Common.Models.Response;
using Application.Models.Response;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services
{
    public interface IUserService
    {
        Task<AuthResult> AuthenticateUser(string email, string password);
        Task<ResetPasswordToken> GetUserByPasswordToken(string passwordToken);
        Task<TeamConnect> GetUserByTeamConnectToken(string teamConnectToken);
        Task<Result> ResetPassword(int userId, string password);
        Task<Result> ForgotPassword(string email, string passwordtoken, string passwordResetLink);
    }
}
