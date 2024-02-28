using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.DataBase
{
    public interface ITeamConnectContext
    {        
        Task<TEntity> ExecuteStoredProcedure<TEntity>(string storedProcedureName, params object[] parameters);
        Task<IList<TEntity>> ExecuteStoredProcedureList<TEntity>(string storedProcedureName, params object[] parameters);
        Task<bool> ExecuteStoredProcedure(string storedProcedureName, params object[] parameters);
        IDbConnection GetDbConnection();
        DynamicParameters GetDapperDynamicParameters(params object[] parameters);
        DataTable LoadDataTable(string procedureName, SqlParameter[] parameters, out List<int> outParas);        
    }
}
