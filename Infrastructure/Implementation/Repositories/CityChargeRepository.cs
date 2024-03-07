using Application.Common.Dtos;
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

        public async Task<string> CreateUpdateCity(string cityXml, CreateUpdateRequestDtoList createUpdateRequestDtoList)
        {
            return await _dbContext.ExecuteStoredProcedure<string>("usp_CreateUpdateCity",
             _parameterManager.Get("UserId", createUpdateRequestDtoList.createUpdateRequestDtos),
             _parameterManager.Get("cityXml", cityXml )
             );
        }

        public async Task<bool> DeleteCity(int cityId)
        {
         return await _dbContext.ExecuteStoredProcedure("usp_DeleteCity",
               _parameterManager.Get("CityId", cityId));
        }
        public async Task<bool> IsExistCity(string Name, int id = 0)
        {
            return await _dbContext.ExecuteStoredProcedure<bool>("IsExistCity",
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
    }
}
        
    

    

