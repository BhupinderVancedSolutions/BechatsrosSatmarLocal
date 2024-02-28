using Application.Common.Interfaces.Services;
using Application.Models.Response;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Authentication.Queries
{
    public class ForgotPasswordCommand : IRequest<Result>
    {
        public string Email { get; init; }
        public string PasswordToken { get; set; }
        public string PasswordResetLink { get; set; }
    }

    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Result>
    {
        private readonly IUserService _userService;

        public ForgotPasswordCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            return await _userService.ForgotPassword(request.Email,request.PasswordToken,request.PasswordResetLink);
        }
    }
}
