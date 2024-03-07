
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Application.Common.Interfaces.Services;
using DTO.Response.CityCharge;

namespace Application.Areas.Queries.GetCityById
{
    public class GetCityByIdQuery:IRequest<CityChargeResponseDto>
    {
        public int CityId { get; init; }
    }
    public class GetCityByIdQueryQueryHandler : IRequestHandler<GetCityByIdQuery, CityChargeResponseDto>
    {
        private readonly ICityChargeService _cityChargeService;

        public GetCityByIdQueryQueryHandler(ICityChargeService cityChargeService)
        {
            _cityChargeService = cityChargeService;
        }

        public async Task<CityChargeResponseDto> Handle(GetCityByIdQuery request, CancellationToken cancellationToken)
        {
            return await _cityChargeService.GetCityById(request.CityId);
        }
    }
}
