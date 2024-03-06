using Application.Common.Dtos;
using Application.Common.Models.Request;
using Application.Models.Response;
using DTO.Request.CityCharge;
using DTO.Response.CityCharge;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services
{
    public interface ICityChargeService
    {
        Task<string> CreateUpdateCity(CreateUpdateRequestDtoList createUpdateRequestDtoList,int UserId);
        Task<bool> DeleteCity(int cityId);
        Task<PaginatedList<CityChargeResponseDto>> GetCities(CommonRequest commonRequest);

    }
}
