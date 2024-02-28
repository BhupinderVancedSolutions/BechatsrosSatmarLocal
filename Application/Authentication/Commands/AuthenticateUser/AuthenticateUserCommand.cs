using Application.Common.Interfaces.Services;
using Application.Models.Response;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Authentication.Commands
{
    public class AuthenticateUserCommand : IRequest<AuthResult>
    {
        public string Email { get; init; }
        public string Password { get; init; }
    }

    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, AuthResult>
    {
        private readonly IUserService _userService;

        public AuthenticateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<AuthResult> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.AuthenticateUser(request.Email, request.Password);
        }
    }
}
