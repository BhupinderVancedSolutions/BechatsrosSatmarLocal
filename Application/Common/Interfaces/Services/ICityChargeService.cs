using Application.Common.Models.Request;
using Application.DTO.Response;
using Application.Models.Response;
using DTO.Request.CityCharge;
using DTO.Response.CityCharge;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services
{
    public interface ICityChargeService
    {
        Task<Result> CreateUpdateCity(CreateUpdateRequestDtoList createUpdateRequestDtoList, int userId);
        Task<Result> DeleteCity(int cityId);
        Task<PaginatedList<CityChargeResponseDto>> GetCities(CommonRequest commonRequest);
        Task<CityChargeResponseDto> GetCityById(int cityId);
        Task<Result> UpdateCity(UpdateRequestDto  updateRequestDto);
    }
}
