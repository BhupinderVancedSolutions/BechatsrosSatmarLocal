using Application.Common.DTO.Request;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User> AuthenticateUser(string email);
        Task<User> ForgotPassword(string email);
        Task UpdateForgotPasswordExpirationDate(int userId, string passwordToken, DateTime passwordExpirationDate);
        Task<User> GetUserByPasswordToken(string passwordToken);
        Task<User> GetUserByTeamConnectToken(string teamConnectToken);
        Task ResetPassword(int userId, byte[] password);
        Task UpdateConfirmEmailExpirationDate(int userId, string passwordToken, DateTime passwordExpirationDate);
    }
}
