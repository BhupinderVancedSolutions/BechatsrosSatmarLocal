using Application.Common.Interfaces.Services;
using Application.DTO.Response;
using DTO.Request.CityCharge;
using Mapster;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Areas.Command.CreateUpdateCity
{
    public class CreateCityCommand : IRequest<Result>
    {
        public string CityName { get; set; }
        public decimal? Price { get; set; }
        public int CityId { get; set; }
    }
    public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, Result>
    {
        private readonly ICityChargeService _cityChargeService;
        public CreateCityCommandHandler(ICityChargeService cityChargeService )
        {
            _cityChargeService = cityChargeService;
        }
        public async Task<Result> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            return await _cityChargeService.CreateCity(request.Adapt<CreateUpdateRequest>());
        }
    }
}
