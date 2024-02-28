using Application.Common.Interfaces.Services;
using Application.Common.Models.Response;
using DTO.Request;
using Infrastructure.Attributes;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Razor.Templating.Core;
using Common.Helper;
using System.Collections.Generic;
using Application.Common.Models.Request;
using System.Globalization;
using Application.Common.Interfaces.ExternalAPI;
using Common.Settings;

namespace Infrastructure.Implementation.Services
{
    [ScopedService]
    public class EmailTemplateService : IEmailTemplateService
    {
        #region Fields

        private readonly AppSettings _appSettings;
        private readonly ISendGridEmail _sendGridEmail;


        #endregion

        #region Ctor
        public EmailTemplateService(IOptions<AppSettings> appSettings, ISendGridEmail sendGridEmail)
        {
            _appSettings = appSettings.Value;
            _sendGridEmail = sendGridEmail;
        }

        #endregion
        public async Task<bool> SendForgotPasswordMail(string name, string email, string passwordResetLink)
        {
            var body = string.Empty;
            var viewModel = new ResetPasswordModel
            {
                ResetPasswordUrl = passwordResetLink,
            };
            body = await RazorTemplateEngine.RenderAsync("/Views/EmailTemplates/ResetPassword.cshtml", viewModel);
            var isMailSent = await _sendGridEmail.SendMail(
                  email, "Password Recovery", body);

            return isMailSent;
        }

    }
}
