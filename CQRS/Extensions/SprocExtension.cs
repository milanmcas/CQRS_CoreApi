using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Globalization;

namespace CQRS.Extensions
{
    /// <summary>
    /// https://medium.com/hackernoon/execute-a-stored-procedure-that-gets-data-from-multiple-tables-in-ef-core-1638a7f010c
    /// </summary>
    public static class SprocExtension
    {
        public static DbCommand LoadStoredProcedure(this DbContext dbContext,string storedProcedure)
        {
            var cmd=dbContext.Database.GetDbConnection().CreateCommand();
            cmd.CommandText=storedProcedure;
            cmd.CommandType=System.Data.CommandType.StoredProcedure;
            return cmd;
        }
        public static DbCommand WithSqlParams(this DbCommand dbCommand, params (string, object)[] values)
        {
            foreach (var value in values) { 
            var param=dbCommand.CreateParameter();
                param.ParameterName = value.Item1;
                param.Value = value.Item2;
                dbCommand.Parameters.Add(param);
            }
            return dbCommand;
        }
        public static IList<T> ExecuteStoredProcedure<T>(this DbCommand command) where T : class
        {
            using (command)
            {
                if (command?.Connection?.State == System.Data.ConnectionState.Closed)
                    command.Connection.Open();
                try
                {
                    if(command is not null)
                    using (var reader = command.ExecuteReader())
                    {
                        return reader.MapToList<T>();
                    }
                    else
                        return new List<T>();
                }
                finally
                {
                    command?.Connection?.Close();
                }

            }
        }
        public static async Task<IList<T>> ExecuteStoredProcedureAsync<T>(this DbCommand command) where T : class
        {
            using (command)
            {
                if (command?.Connection?.State == System.Data.ConnectionState.Open)
                    await command.Connection.OpenAsync();
                try
                {
                    if (command is not null)
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            return reader.MapToList<T>();
                        }
                    else
                        return new List<T>();
                }
                finally
                {
                    command?.Connection?.Close();
                }
            }
        }
        private static IList<T> MapToList<T>(this DbDataReader reader) 
        {
            var objList=new List<T>();
            var props=typeof(T).GetProperties();
            var colMapping = reader.GetColumnSchema()
                .Where(x => props.Any(y => y.Name.ToLower() == x.ColumnName.ToLower()))
                .ToDictionary(key => key.ColumnName.ToLower());
            if (reader.HasRows && colMapping is not null && props is not null)
            {
                while (reader.Read())
                {
                    T obj = Activator.CreateInstance<T>();                    
                    foreach (var prop in props) {
                        if (prop is not null && prop?.Name is not null && prop?.Name!="" 
                            )
                        {
                            if (colMapping.ContainsKey(prop.Name))
                            {
                                var val = reader.GetValue(colMapping[prop.Name.ToLower()].ColumnOrdinal.Value);
                                prop.SetValue(obj, val == DBNull.Value ? null : val);
                            }
                            
                        }                        
                    }
                    objList.Add(obj);
                }
            }
            return objList;
        }
    }
}
