using Application.Common.Dtos;
using Application.Common.Models.Request;
using Application.DTO.Response;
using DTO.Request.CityCharge;
using DTO.Response.CityCharge;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Repositories
{
    public interface ICityChargeRepository
    {
        Task<int> CreateUpdateCity(string CityXml, int userId);
        Task<bool> DeleteCity(int cityId);
        Task<(List<CityChargeResponseDto>, int)> GetCities(CommonRequest commonRequest);
        Task<bool> IsExistCity(string Name, int id = 0);
        Task<CityChargeResponseDto> GetCityById(int cityId);
        Task<int> UpdateCity(UpdateRequestDto  updateRequestDto);
        Task<int> CreateCity(CreateUpdateRequest createUpdateRequest);

    }
}
