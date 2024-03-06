using Application.Common.Interfaces.Services;
using Application.Common.Models.Request;
using Application.Models.Response;
using DTO.Response.CityCharge;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Authentication.Queries.CityCharge
{
    public class GetCitiesQuery : IRequest<PaginatedList<CityChargeResponseDto>>
    {
        public CommonRequest CommonRequest { get; set; }
    }
    public class GetCitiesQueryQueryHandler : IRequestHandler<GetCitiesQuery, PaginatedList<CityChargeResponseDto>>
    {
        private readonly ICityChargeService _cityChargeService;

        public GetCitiesQueryQueryHandler(ICityChargeService cityChargeService)
        {
            _cityChargeService = cityChargeService;
        }

        public async Task<PaginatedList<CityChargeResponseDto>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
        {
            return await _cityChargeService.GetCities(request.CommonRequest);
        }
    }
}