using Application.Common.Interfaces.Services;
using Application.DTO.Response;
using DTO.Request.CityCharge;
using Mapster;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Areas.Command.CreateCategory
{
    public class CreateUpdateCityCommand : IRequest<Result>
    {
        public List<CreateUpdateRequestDto> createUpdateRequestDtos { get; set; }

    }

    public class CreateUpdateRequestDto
    {
        public int UserId { get; set; }
        public string CityName { get; set; }
        public decimal? Price { get; set; }
        public int CityId { get; set; }
    }
    public class CreateUpdateCityCommandHandler : IRequestHandler<CreateUpdateCityCommand, Result>
    {
        private readonly ICityChargeService _cityChargeService ;

        public CreateUpdateCityCommandHandler(ICityChargeService  cityChargeService)
        {
            _cityChargeService = cityChargeService;
        }

        public async Task<Result> Handle(CreateUpdateCityCommand request, CancellationToken cancellationToken)
        {
            return await _cityChargeService.CreateUpdateCity(request.Adapt<CreateUpdateRequestDtoList>(), request.createUpdateRequestDtos[0].UserId);
               
        }
    }


}
