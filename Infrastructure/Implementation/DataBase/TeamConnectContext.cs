using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using static Dapper.SqlMapper;
using System.Data.SqlClient;
using Application.Common.Interfaces.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Implementation.DataBase
{
    public class TeamConnectContext : ITeamConnectContext
    {
        private readonly string _connectionString;
        public TeamConnectContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<TEntity> ExecuteStoredProcedure<TEntity>(string storedProcedureName, params object[] parameters)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var entity = await db.QueryAsync<TEntity>(storedProcedureName, GetDapperDynamicParameters(parameters), commandType: CommandType.StoredProcedure);
            return entity.FirstOrDefault();
        }

        public async Task<IList<TEntity>> ExecuteStoredProcedureList<TEntity>(string storedProcedureName, params object[] parameters)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var entities = await db.QueryAsync<TEntity>(storedProcedureName, GetDapperDynamicParameters(parameters), commandType: CommandType.StoredProcedure);
            return entities.ToList();
        }

        public async Task<bool> ExecuteStoredProcedure(string storedProcedureName, params object[] parameters)
        {
            bool isRecordInserted = false;
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.QueryAsync(storedProcedureName, GetDapperDynamicParameters(parameters), commandType: CommandType.StoredProcedure);
                isRecordInserted = true;
            }
            return isRecordInserted;
        }

        public IDbConnection GetDbConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public DynamicParameters GetDapperDynamicParameters(params object[] parameters)
        {
            var dynamicParams = new DynamicParameters();
            for (int index = 0; index <= parameters.Length - 1; index++)
            {
                var item = ((System.Data.SqlClient.SqlParameter)parameters[index]);
                dynamicParams.Add(item.ParameterName, item.Value, item.DbType, item.Direction);
            }
            return dynamicParams;
        }
        public DataTable LoadDataTable(string procedureName, System.Data.SqlClient.SqlParameter[] parameters, out List<int> outParas)
        {
            DataTable dataTable = new();
            outParas = new List<int>();

            using (var sqlConnection = new SqlConnection(_connectionString))
            using (var sqlCommand = new SqlCommand(procedureName, sqlConnection))
            {
                sqlCommand.Parameters.Add(parameters);
                using (var sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlDataAdapter.Fill(dataTable);
                }
                foreach (var param in parameters)
                {
                    if (param.Direction == ParameterDirection.InputOutput ||
                            param.Direction == ParameterDirection.Output)
                    {
                        if (int.TryParse(param.Value.ToString(), out int outPara))
                            outParas.Add(outPara);
                    }
                }
            }
            return dataTable;
        }

    }
}
