
using Application.Common.Interfaces.Services;
using Application.DTO.Response;
using DTO.Request.CityCharge;
using Mapster;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Areas.Command.UpdateCity
{
    public class UpdateCityCommand : IRequest<Result>
    {
        public int UserId { get; set; }
        public string CityName { get; set; }
        public decimal? Price { get; set; }
        public int CityId { get; set; }
    }
    public class UpdateCityCommandHandler : IRequestHandler<UpdateCityCommand, Result>
    {
        private readonly ICityChargeService _cityChargeService;

        public UpdateCityCommandHandler(ICityChargeService cityChargeService)
        {
            _cityChargeService = cityChargeService;
        }
        public async  Task<Result> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
        {
            return await _cityChargeService.UpdateCity(request.Adapt<UpdateRequestDto>());
        }
    }


}
