using Application.Common.Models.Response;
using Application.Common.Interfaces.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Authentication.Queries
{
    public class ResetPasswordQuery : IRequest<ResetPasswordToken>
    {
        public string PasswordToken { get; init; }
    }

    public class ResetPasswordQueryHandler : IRequestHandler<ResetPasswordQuery, ResetPasswordToken>
    {
        private readonly IUserService _userService;

        public ResetPasswordQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ResetPasswordToken> Handle(ResetPasswordQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetUserByPasswordToken(request.PasswordToken);
        }
    }
}
