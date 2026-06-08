using FEMOS.Rentora.Domain.Entities;
using FEMOS.Rentora.Domain.Responses;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Infrastructure.Interfaces
{
    public interface IDBHelper
    {
        Task<DataTable> GetDataTableByQuery(string query);
        Task<DataTable> GetDataTableBySQLCommandAsync(SqlCommand cmd);
        Task<DataSet> GetDataSetBySQLCommandAsync(SqlCommand cmd);
        Task<int> ExecuteNonQueryBySQLCommandAsync(SqlCommand cmd);
        Task<string> ExecuteScalarBySQLCommand(SqlCommand cmd);
        List<T> ConvertDataTable<T>(DataTable dt);
        Task<DBResponseInfo> GetDBResponse(string result);
    }
}
