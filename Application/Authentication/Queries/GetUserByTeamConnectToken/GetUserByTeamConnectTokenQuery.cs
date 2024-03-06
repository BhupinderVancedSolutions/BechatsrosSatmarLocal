using Application.Common.DTO.Response;
using Application.Common.Interfaces.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Authentication.Queries
{
    public class GetUserByTeamConnectTokenQuery : IRequest<TeamConnect>
    {
        public string TeamConnectToken { get; init; }
    }

    public class GetUserByTeamConnectTokenQueryHandler : IRequestHandler<GetUserByTeamConnectTokenQuery, TeamConnect>
    {
        private readonly IUserService _userService;

        public GetUserByTeamConnectTokenQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<TeamConnect> Handle(GetUserByTeamConnectTokenQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetUserByTeamConnectToken(request.TeamConnectToken);
        }
    }
}
