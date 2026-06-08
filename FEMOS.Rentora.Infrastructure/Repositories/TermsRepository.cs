using FEMOS.Rentora.Domain.Constants;
using FEMOS.Rentora.Domain.Entities;
using FEMOS.Rentora.Domain.Requests;
using FEMOS.Rentora.Domain.Responses;
using FEMOS.Rentora.Infrastructure.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Infrastructure.Repositories
{
    internal class TermsRepository : ITermsRepository
    {
        private readonly IDBHelper _dbHelper;

        public TermsRepository(IDBHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<TermsInfo?> GetCurrentTermsAsync(string appCode, string termsType)
        {
            var cmd = new SqlCommand(DBConstants.sp_GetCurrentTerms);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AppCode", appCode);
            cmd.Parameters.AddWithValue("@TermsType", termsType);

            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            if (dt.Rows.Count == 0) return null;

            var row = dt.Rows[0];
            var info = new TermsInfo
            {
                Version = dt.Columns.Contains("Version") && row["Version"] != DBNull.Value ? Convert.ToInt32(row["Version"]) : 0,
                Title = dt.Columns.Contains("Title") ? row["Title"]?.ToString() ?? string.Empty : string.Empty,
                Content = dt.Columns.Contains("Content") ? row["Content"]?.ToString() ?? string.Empty : string.Empty,
                IsMajorUpdate = dt.Columns.Contains("IsMajorUpdate") && row["IsMajorUpdate"] != DBNull.Value && Convert.ToBoolean(row["IsMajorUpdate"]),
                EffectiveFrom = dt.Columns.Contains("EffectiveFrom") && row["EffectiveFrom"] != DBNull.Value ? Convert.ToDateTime(row["EffectiveFrom"]).ToString("yyyy-MM-ddTHH:mm:ssZ") : string.Empty
            };

            return info;
        }

        public async Task<TermsStatusResponseInfo?> CheckUserTermsStatusAsync(string appCode, string termsType, Guid userPublicId)
        {
            var cmd = new SqlCommand(DBConstants.sp_CheckUserTermsStatus);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AppCode", appCode);
            cmd.Parameters.AddWithValue("@TermsType", termsType);
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);

            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            if (dt.Rows.Count == 0) return null;

            var row = dt.Rows[0];
            return new TermsStatusResponseInfo
            {
                CurrentVersion = dt.Columns.Contains("CurrentVersion") && row["CurrentVersion"] != DBNull.Value ? Convert.ToInt32(row["CurrentVersion"]) : 0,
                UserVersion = dt.Columns.Contains("UserVersion") && row["UserVersion"] != DBNull.Value ? Convert.ToInt32(row["UserVersion"]) : 0,
                IsAcceptanceRequired = dt.Columns.Contains("IsAcceptanceRequired") && row["IsAcceptanceRequired"] != DBNull.Value && Convert.ToBoolean(row["IsAcceptanceRequired"])
            };
        }

        public async Task<AcceptTermsResponseInfo> AcceptTermsAsync(AcceptTermsRequestInfo model)
        {
            var cmd = new SqlCommand(DBConstants.sp_SaveUserTermsAcceptance);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AppCode", model.AppCode);
            cmd.Parameters.AddWithValue("@TermsVersion", model.TermsVersion);
            cmd.Parameters.AddWithValue("@TermsType", model.TermsType);
            cmd.Parameters.AddWithValue("@AcceptedVia", (object?)model.AcceptedVia ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DeviceId", (object?)model.DeviceId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DeviceName", (object?)model.DeviceName ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@IpAddress", (object?)model.IpAddress ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UserAgent", (object?)model.UserAgent ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UserPublicId", model.UserPublicId);

            var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            var dbResponse = await _dbHelper.GetDBResponse(result);

            return new AcceptTermsResponseInfo { Success = dbResponse.Status == "Success", Message = dbResponse.Message };
        }

        public async Task<ValidateTermsResponseInfo> ValidateUserTermsAsync(string appCode, Guid userPublicId)
        {
            var cmd = new SqlCommand(DBConstants.sp_ValidateUserTerms);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AppCode", appCode);
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);

            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            if (dt.Rows.Count == 0)
                return new ValidateTermsResponseInfo { IsValid = false, Message = "TERMS_NOT_FOUND" };

            var row = dt.Rows[0];
            return new ValidateTermsResponseInfo
            {
                IsValid = row.Table.Columns.Contains("IsValid") && row["IsValid"] != DBNull.Value && Convert.ToBoolean(row["IsValid"]),
                Message = row.Table.Columns.Contains("Message") ? row["Message"]?.ToString() ?? string.Empty : string.Empty
            };
        }
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              