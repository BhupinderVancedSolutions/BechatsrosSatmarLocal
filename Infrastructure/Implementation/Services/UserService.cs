using Application.Common.DTO.Response;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.DTO.Response;
using Infrastructure.Attributes;
using System.Threading.Tasks;
using Mapster;
using Common.Helper;
using System;
using Microsoft.Extensions.Options;
using Common.Settings;

namespace Infrastructure.Implementation.Services
{
    [ScopedService]
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly AppSettings _appSettings;
        private readonly IEmailTemplateService _emailTemplateService;
        public UserService(IUserRepository userRepository, IOptions<AppSettings> appSettings, IEmailTemplateService emailTemplateService)
        {
            _userRepository = userRepository;
            _appSettings = appSettings.Value;
            _emailTemplateService = emailTemplateService;

        }

        public async Task<AuthResult> AuthenticateUser(string email, string password)
        {
            var user = await _userRepository.AuthenticateUser(email);

            if (user == null)
                return new AuthResult { IsSuccess = false, ErrorMessage = "User dosen't exists." };

            string pwd = EncryptionHelper.DecryptByteArrayToString(user.Password, _appSettings.Secret);
            if (password != pwd)
                return new AuthResult { IsSuccess = false, ErrorMessage = "Invalid password." };

            return new AuthResult { IsSuccess = true, Result = user.Adapt<ApplicationUserResponse>(), ErrorMessage = string.Empty };
        }

        public async Task<Result> ForgotPassword(string email, string passwordtoken, string passwordResetLink)
        {
            Result result;
            var user = await _userRepository.ForgotPassword(email);
            if (user != null && user.UserId > 0)
            {
                user.PasswordExpirationDate = DateTime.Today.AddDays(2);
                user.PasswordToken = passwordtoken;
                await _emailTemplateService.SendForgotPasswordMail(user.FirstName, user.Email, passwordResetLink);
                await _userRepository.UpdateForgotPasswordExpirationDate(user.UserId, passwordtoken, DateTime.UtcNow.AddHours(_appSettings.ExpirationHours));
                result = Result.Success(new string[] { "Please check you email for password Reset." });
            }
            else
            {
                result = Result.Failure(new string[] { "Email does not Exist." });
            }
            return result;
        }

        public async Task<ResetPasswordToken> GetUserByPasswordToken(string passwordToken)
        {
            var user = await _userRepository.GetUserByPasswordToken(passwordToken);
            return user.Adapt<ResetPasswordToken>();
        }
        public async Task<TeamConnect> GetUserByTeamConnectToken(string teamConnectToken)
        {
            var user = await _userRepository.GetUserByTeamConnectToken(teamConnectToken);
            return user.Adapt<TeamConnect>();
        }
        public async Task<Result> ResetPassword(int userId, string password)
        {
            await _userRepository.ResetPassword(userId, EncryptionHelper.EncryptStringToByteArray(password, _appSettings.Secret));
            return Result.Success(new string[] { "Password updated successfully" });
        }

    }
}
