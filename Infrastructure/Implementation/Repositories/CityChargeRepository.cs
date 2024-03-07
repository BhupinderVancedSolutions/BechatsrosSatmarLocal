
using Application.Common.Interfaces.DataBase;
using Application.Common.Interfaces.Repositories;
using Application.Common.Models.Request;
using Dapper;
using DTO.Request.CityCharge;
using DTO.Response.CityCharge;
using Infrastructure.Attributes;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Implementation.Repositories
{
    [ScopedService]
    public class CityChargeRepository : ICityChargeRepository
    {
        private readonly IBechatsrosSatmar _dbContext;
        private readonly IParameterManager _parameterManager;
        public CityChargeRepository(IBechatsrosSatmar dbContext, IParameterManager parameterManager)
        {
            _dbContext = dbContext;
            _parameterManager = parameterManager;
        }

        public async Task<int> CreateUpdateCity(string cityXml, int userId)
        {
            var dd =  await _dbContext.ExecuteStoredProcedure<int>("usp_CreateUpdateCity",
             _parameterManager.Get("UserId", userId),
             _parameterManager.Get("cityXml", cityXml )
             );
            return dd;
        }

        public async Task<bool> DeleteCity(int cityId)
        {

                return await _dbContext.ExecuteStoredProcedure("usp_DeleteCity",
              _parameterManager.Get("CityId", cityId));

        
        }
        public async Task<bool> IsExistCity(string Name, int id = 0)
        {
            return await _dbContext.ExecuteStoredProcedure<bool>("usp_IsCityExist",
                _parameterManager.Get("Id", id),
                 _parameterManager.Get("Name", Name));
        }

        public async Task<(List<CityChargeResponseDto>, int)> GetCities(CommonRequest commonRequest)
        {
            List<CityChargeResponseDto> categorys;
            int total = 0;
            using (var dbConnection = _dbContext.GetDbConnection())
            {
                var result = await dbConnection.QueryMultipleAsync(
                  "usp_GetCities", _dbContext.GetDapperDynamicParameters
                  (_parameterManager.Get("StartRow", commonRequest.StartRow),
                 _parameterManager.Get("EndRow", commonRequest.EndRow),
                 _parameterManager.Get("FilterQuery", commonRequest.FilterQuery),
                 _parameterManager.Get("OrderBy", commonRequest.OrderBy),
                  _parameterManager.Get("SearchText", commonRequest.SearchText)),
                  commandType: CommandType.StoredProcedure);
                total = result.Read<int>().FirstOrDefault();
                categorys = result.Read<CityChargeResponseDto>().ToList();
            }
            return (categorys, total);
        }
        public async Task<int> IsCityExist(int cityId,string cityName)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_IsCityExist",
                _parameterManager.Get("CityId", cityId),
                 _parameterManager.Get("CityName", cityName)
               );
        }

        public async Task<int> UpdateCity(UpdateRequestDto cityChargeRequestDto)
        {
            return await _dbContext.ExecuteStoredProcedure<int>("usp_UpdateCity",
                _parameterManager.Get("CityId", cityChargeRequestDto.CityId),
                 _parameterManager.Get("CityName", cityChargeRequestDto.CityName),
                 _parameterManager.Get("Price", cityChargeRequestDto.Price)
               );
        }

        public async Task<CityChargeResponseDto> GetCityById(int cityId)
        {
            return await _dbContext.ExecuteStoredProcedure<CityChargeResponseDto>("usp_GetCityById",
                _parameterManager.Get("CityId", cityId));
        }
    }
}
        
    

    

