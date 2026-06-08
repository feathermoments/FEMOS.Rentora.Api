using FEMOS.Rentora.Domain.Constants;
using FEMOS.Rentora.Domain.Entities;
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
    internal class AuthRepository : IAuthRepository
    {
        private readonly IDBHelper _dbHelper;

        public AuthRepository(IDBHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<SendOtpResponseInfo> SendOtpAsync(SendOtpInfo model, string OTPCode)
        {
            var cmd = new SqlCommand(DBConstants.sp_SendOtp);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Contact", model.Identifier);
            cmd.Parameters.AddWithValue("@ContactHash", model.ContactHash);
            cmd.Parameters.AddWithValue("@ContactEncrypted", model.ContactEncrypted);
            cmd.Parameters.AddWithValue("@OTPMode", model.Type);
            cmd.Parameters.AddWithValue("@OtpHash", model.OtpHash);
            cmd.Parameters.AddWithValue("@OtpEncrypted", model.OtpEncrypted);
            cmd.Parameters.AddWithValue("@OTPCode", OTPCode);

            string result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            DBResponseInfo dbResponse = await _dbHelper.GetDBResponse(result.Split(',').Last());

            return new SendOtpResponseInfo
            {
                isExistingUser = result.Split(",").FirstOrDefault() != "0",
                Status = dbResponse.Status,
                Message = dbResponse.Message
            };
        }

        public async Task<DBAuthResponseInfo> VerifyOtpAsync(VerifyOtpInfo model)
        {
            var cmd = new SqlCommand(DBConstants.sp_VerifyOtp);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ContactHash", model.ContactHash);
            cmd.Parameters.AddWithValue("@ContactEncrypted", model.ContactEncrypted);
            cmd.Parameters.AddWithValue("@OTPMode", model.Type);
            cmd.Parameters.AddWithValue("@OtpHash", model.OtpHash);

            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);

            if (dt.Rows.Count == 0)
            {
                return new DBAuthResponseInfo
                {
                    Status = StatusConstants.Failure,
                    Message = "Invalid OTP or mobile number."
                };
            }

            var row = dt.Rows[0];
            var userPublicId = row.Table.Columns.Contains("UserPublicId") ? (Guid)row["UserPublicId"] : Guid.Empty;
            var role = row.Table.Columns.Contains("Role") ? row["Role"]?.ToString() ?? "User" : "User";
            var isNewUser = row.Table.Columns.Contains("IsNewUser") && Convert.ToBoolean(row["IsNewUser"]);
            bool IsProfileComplete = row.Table.Columns.Contains("IsProfileComplete") && Convert.ToBoolean(row["IsProfileComplete"]);
            if (userPublicId != Guid.Empty)
            {
                return new DBAuthResponseInfo
                {
                    Status = StatusConstants.Success,
                    Message = ApiConstants.Successfull,
                    UserPublicId = userPublicId,
                    Role = role,
                    IsNewUser = isNewUser,
                    IsProfileComplete = IsProfileComplete
                };
            }
            else
            {
                return new DBAuthResponseInfo
                {
                    Status = StatusConstants.Failure,
                    Message = MessageConstants.InvalidUser,
                    UserPublicId = userPublicId,
                    IsNewUser = isNewUser,
                    IsProfileComplete = IsProfileComplete
                };
            }
        }
    }
}
