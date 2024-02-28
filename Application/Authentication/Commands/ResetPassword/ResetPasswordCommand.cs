using Application.Common.Interfaces.Services;
using Application.Models.Response;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Authentication.Commands
{
    public class ResetPasswordCommand : IRequest<Result>
    {
        public int UserId { get; init; }
        public string Password { get; init; }
    }
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result>
    {
        private readonly IUserService _userService;

        public ResetPasswordCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            return await _userService.ResetPassword(request.UserId, request.Password);
        }
    }
}
