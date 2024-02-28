using Application.Common.Models.Request;
using Domain.Entities;
using Application.Common.Interfaces.Repositories;
using Infrastructure.Attributes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Interfaces.DataBase;
using System.Data;
using Dapper;
using System.Linq;

namespace Infrastructure.Implementation.Repositories
{
    [ScopedService]
    public class UserRepository : IUserRepository
    {
        private readonly ITeamConnectContext _dbContext;
        private readonly IParameterManager _parameterManager;        
        public UserRepository(ITeamConnectContext dbContext, IParameterManager parameterManager) 
        {
            _dbContext = dbContext;
            _parameterManager = parameterManager;            
        }

        public async Task<User> AuthenticateUser(string email)
        {
            return await _dbContext.ExecuteStoredProcedure<User>("usp_UserLogin",
                _parameterManager.Get("Email", email));
        }

        public async Task<User> ForgotPassword(string email)
        {
            return await _dbContext.ExecuteStoredProcedure<User>("usp_IsEmailExist",
                _parameterManager.Get("Email", email));
        }

        public async Task UpdateForgotPasswordExpirationDate(int userId, string passwordToken, DateTime passwordExpirationDate)
        {
            await _dbContext.ExecuteStoredProcedure("usp_UpdateForgotPasswordExpirationDate",
              _parameterManager.Get("UserId", userId),
              _parameterManager.Get("PasswordToken", passwordToken),
              _parameterManager.Get("passwordExpirationDate", passwordExpirationDate, ParameterDirection.Input, DbType.DateTime2));
        }

        public async Task<User> GetUserByPasswordToken(string passwordToken)
        {
            return await _dbContext.ExecuteStoredProcedure<User>("usp_GetUserByPasswordToken",
               _parameterManager.Get("PasswordToken", passwordToken));
        }
        public async Task<User> GetUserByTeamConnectToken(string teamConnectToken)
        {
            return await _dbContext.ExecuteStoredProcedure<User>("usp_GetUserByTeamConnectToken",
               _parameterManager.Get("TeamConnectToken", teamConnectToken));
        }
        public async Task ResetPassword(int userId, byte[] password)
        {
            await _dbContext.ExecuteStoredProcedure("usp_ResetPassword",
              _parameterManager.Get("UserId", userId),
              _parameterManager.Get("Password", password, ParameterDirection.Input, DbType.Binary));
        }


        public async Task UpdateConfirmEmailExpirationDate(int userId, string passwordToken, DateTime passwordExpirationDate)
        {

            await _dbContext.ExecuteStoredProcedure("usp_UpdateForgotPasswordExpirationDate",
           _parameterManager.Get("UserId", userId),
           _parameterManager.Get("PasswordToken", passwordToken),
           _parameterManager.Get("passwordExpirationDate", passwordExpirationDate, ParameterDirection.Input, DbType.DateTime2));


        }
    }
}
