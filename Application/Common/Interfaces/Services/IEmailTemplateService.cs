
using Application.Common.Models.Request;
using Application.Common.Models.Response;
using DTO.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services
{
    public interface IEmailTemplateService
    {
        #region User
        public Task<bool> SendForgotPasswordMail(string name, string email, string passwordResetLink);

        #endregion
    }
}
