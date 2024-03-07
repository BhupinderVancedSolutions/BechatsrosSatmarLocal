using Application.Common.Dtos;
using Application.Common.Models.Request;
using DTO.Request.CityCharge;
using DTO.Response.CityCharge;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Repositories
{
    public interface ICityChargeRepository
    {
        Task<string> CreateUpdateCity(string CityXml, CreateUpdateRequestDtoList createUpdateRequestDtoList);
        Task<bool> DeleteCity(int cityId);
        Task<(List<CityChargeResponseDto>, int)> GetCities(CommonRequest commonRequest);
        Task<bool> IsExistCity(string Name, int id = 0);

    }
}
