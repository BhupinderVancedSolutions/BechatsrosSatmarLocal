using Application.Common.Interfaces.Services;
using Application.DTO.Response;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Areas.Command.DeleteCity
{
    public class DeleteCityCommand : IRequest<Result>
    {
        public int CityId { get; init; }
    }

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCityCommand, Result>
    {
        private readonly ICityChargeService  _cityChargeService;

        public DeleteCategoryCommandHandler(ICityChargeService  cityChargeService)
        {
            _cityChargeService = cityChargeService;
        }

        public async Task<Result> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
        {
            return await _cityChargeService.DeleteCity(request.CityId);
        }
    }
}
