using Application.Common.Interfaces.DataBase;
using Common.Helper;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Implementation.DataBase
{
    public class ParameterManager : IParameterManager
    {

        public IDataParameter Get(object value) => Get("Id", value);
        public IDataParameter Get(string paramName, object value)
        {
            return Get(paramName, value, ParameterDirection.Input, DbType.String);
        }       
        public IDataParameter Get(string paramName, object value, ParameterDirection direction)
        {
            return Get(paramName, value, direction, DbType.String);
        }
        public IDataParameter Get(string paramName, object value, ParameterDirection direction, DbType type)
        {
            IDataParameter param = new SqlParameter
            {
                ParameterName = paramName,
                Value =paramName== "FilterQuery"? AgGridHelper.GetWhereSql(value!=null?value.ToString():"") : value,
                Direction = direction,
                DbType = type
            };
            return (SqlParameter)param;
        }
        public IDataParameter GetNew(string paramName, object value, ParameterDirection direction, SqlDbType type)
        {
            var param = new SqlParameter
            {
                ParameterName = paramName,
                Value = value,
                Direction = direction,
                SqlDbType = type
            };
            return param;
        }

        public SqlParameter GetSql(string name, SqlDbType type, int? size, object paramValue)
        {
            SqlParameter param;

            param = ((size != null)) ? new SqlParameter(name, type, (int)size) : new SqlParameter(name, type);
            param.Direction = ParameterDirection.Input;

            if (paramValue == null)
            {
                param.Value = DBNull.Value;
            }
            else
            {
                if (paramValue is DateTime val)
                    if (val == DateTime.MinValue)
                        paramValue = DBNull.Value;

                param.Value = paramValue;
            }

            return param;
        }
        public SqlParameter GetSql(string name, SqlDbType type, object paramValue)
        {
            return GetSql(name, type, null, paramValue);
        }
        public SqlParameter GetSqlOut(string name, SqlDbType type, object paramValue)
        {
            return GetSqlOut(name, type, null, paramValue);
        }
        public SqlParameter GetSqlOut(string name, SqlDbType type, int? size, object paramValue)
        {
            SqlParameter param;

            param = size != null ? new SqlParameter(name, type, (int)size) : new SqlParameter(name, type);
            param.Direction = ParameterDirection.Output;

            if (paramValue == null)
            {
                param.Value = DBNull.Value;
            }
            else
            {
                if (paramValue is DateTime val)
                    if (val == DateTime.MinValue)
                        paramValue = DBNull.Value;

                param.Value = paramValue;
            }

            return param;
        }
    }
}
