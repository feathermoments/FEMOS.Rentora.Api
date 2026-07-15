using FEMOS.Rentora.Domain.Constants;
using FEMOS.Rentora.Domain.Entities;
using FEMOS.Rentora.Domain.Responses;
using FEMOS.Rentora.Infrastructure.Interfaces;
using FEMOS.Rentora.Shared.Constants;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Infrastructure.Persistance
{
    public class DBHelper : IDBHelper
    {
        private readonly IDbConnectionFactory _connectionFactory;
        public DBHelper(IConfiguration configuration, IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<DataTable> GetDataTableByQuery(string query)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = (SqlConnection)_connectionFactory.CreateConnection())
            {
                await con.OpenAsync();
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.Fill(dt);
                return dt;
            }
        }
        public async Task<DataTable> GetDataTableBySQLCommandAsync(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = (SqlConnection)_connectionFactory.CreateConnection())
                {
                    await con.OpenAsync();
                    cmd.Connection = con;
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    await Task.Run(() => da.Fill(dt));
                    return dt;
                }
            }
            catch
            {
                throw;
            }
        }
        public async Task<DataSet> GetDataSetBySQLCommandAsync(SqlCommand cmd)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection con = (SqlConnection)_connectionFactory.CreateConnection())
                {
                    await con.OpenAsync();
                    cmd.Connection = con;
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    await Task.Run(() => da.Fill(ds));
                    return ds;
                }
            }
            catch
            {
                throw;
            }
        }
        public async Task<int> ExecuteNonQueryBySQLCommandAsync(SqlCommand cmd)
        {
            using (SqlConnection con = (SqlConnection)_connectionFactory.CreateConnection())
            {
                await con.OpenAsync();
                cmd.Connection = con;
                cmd.CommandTimeout = 20;//seconds
                int result = await cmd.ExecuteNonQueryAsync();
                con.Close();
                return result;
            }
        }
        public async Task<string> ExecuteScalarBySQLCommand(SqlCommand cmd)
        {
            using (SqlConnection con = (SqlConnection)_connectionFactory.CreateConnection())
            {
                await con.OpenAsync();
                cmd.Connection = con;
                var result = cmd.ExecuteScalar();
                return Convert.ToString(result);
            }
        }
        public List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                // 1. Direct lookup instead of a nested loop
                PropertyInfo pro = temp.GetProperty(column.ColumnName);

                // Ensure the property exists and can actually be written to
                if (pro != null && pro.CanWrite)
                {
                    object value = dr[column.ColumnName];

                    // 2. Handle Database Nulls safely
                    if (value == DBNull.Value)
                    {
                        pro.SetValue(obj, null, null);
                        continue;
                    }

                    try
                    {
                        // 3. Proactively handle type conversions (Enums, GUIDs, Nullables)
                        object convertedValue = GetValueByDataType(pro.PropertyType, value);
                        pro.SetValue(obj, convertedValue, null);
                    }
                    catch (Exception ex)
                    {
                        // Handle or log your exception here
                    }
                }
            }
            return obj;
        }
        private static object GetValueByDataType(Type propertyType, object o)
        {
            if (string.IsNullOrEmpty(o.ToString()))
            {
                if (propertyType == typeof(Guid) || propertyType == typeof(Guid?))
                {
                    return Guid.NewGuid();
                }
                else if (propertyType == typeof(int) || propertyType.IsEnum)
                {
                    return 0;
                }
                else if (propertyType == typeof(decimal))
                {
                    return 0;
                }
                else if (propertyType == typeof(long))
                {
                    return 0;
                }
                else if (propertyType == typeof(bool) || propertyType == typeof(bool?))
                {
                    return 0;
                }
                else if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?))
                {
                    return 0;
                }
                else if (propertyType == typeof(double))
                {
                    return 0;
                }
                else
                {
                    return Convert.ToString(o);
                }
            }
            else
            {
                if (propertyType == typeof(Guid) || propertyType == typeof(Guid?))
                {
                    return Guid.Parse(o.ToString());
                }
                else if (propertyType == typeof(int) || propertyType.IsEnum)
                {
                    return Convert.ToInt32(o);
                }
                else if (propertyType == typeof(decimal))
                {
                    return Convert.ToDecimal(o);
                }
                else if (propertyType == typeof(long))
                {
                    return Convert.ToInt64(o);
                }
                else if (propertyType == typeof(bool) || propertyType == typeof(bool?))
                {
                    return Convert.ToBoolean(o);
                }
                else if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?))
                {
                    return Convert.ToDateTime(o);
                }
                else if (propertyType == typeof(double))
                {
                    return Convert.ToInt32(o);
                }
            }
            return o.ToString();
        }
        public async Task<DBResponseInfo> GetDBResponse(string result)
        {
            DBResponseInfo objDBResponseInfo;
            if (!string.IsNullOrEmpty(result))
            {
                if (result.Split(':').Length == 2)
                {
                    objDBResponseInfo = new DBResponseInfo()
                    {
                        Status = result.Split(':')[0],
                        Message = result.Split(':')[1]
                    };
                }
                else
                {
                    objDBResponseInfo = new DBResponseInfo()
                    {
                        Status = StatusConstants.Failure,
                        Message = result
                    };
                }
            }
            else
            {
                objDBResponseInfo = new DBResponseInfo()
                {
                    Status = StatusConstants.Failure,
                    Message = ApiConstants.SomethingWentWrong
                };
            }

            return objDBResponseInfo;
        }
    }
}
