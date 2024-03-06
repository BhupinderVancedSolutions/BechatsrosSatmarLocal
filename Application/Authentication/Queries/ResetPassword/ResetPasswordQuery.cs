using Application.Common.DTO.Response;
using Application.Common.Interfaces.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Authentication.Queries
{
    public class ResetPasswordQuery : IRequest<ResetPasswordTokenResponseDto>
    {
        public string PasswordToken { get; init; }
    }

    public class ResetPasswordQueryHandler : IRequestHandler<ResetPasswordQuery, ResetPasswordTokenResponseDto>
    {
        private readonly IUserService _userService;

        public ResetPasswordQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ResetPasswordTokenResponseDto> Handle(ResetPasswordQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetUserByPasswordToken(request.PasswordToken);
        }
    }
}
