using System.Data;

namespace Application.Common.Interfaces.DataBase
{
    public interface IParameterManager
    {
        IDataParameter Get(object value);
        IDataParameter Get(string paramName, object value);
        IDataParameter Get(string paramName, object value, ParameterDirection direction);
        IDataParameter Get(string paramName, object value, ParameterDirection direction, DbType type);
        IDataParameter GetNew(string paramName, object value, ParameterDirection direction, SqlDbType type);
    }
}
