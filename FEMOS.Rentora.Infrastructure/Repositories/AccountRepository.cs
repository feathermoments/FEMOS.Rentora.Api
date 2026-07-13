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
    public class AccountRepository : IAccountRepository
    {
        private readonly IDBHelper _dbHelper;
        public AccountRepository(IDBHelper dbHelper) 
        {
            _dbHelper = dbHelper;
        }

        public async Task<UserAccountResponseInfo> CreateUserAccountAsync(UserAccountInfo objRequestInfo)
        {
            var cmd = new SqlCommand(DBConstants.usp_CreateUserAccount);
            cmd.CommandType = CommandType.StoredProcedure;

            var userIdParam = new SqlParameter("@UserId", SqlDbType.BigInt)
            {
                Direction = ParameterDirection.InputOutput,
                Value = (object?)objRequestInfo.UserId ?? DBNull.Value
            };
            cmd.Parameters.Add(userIdParam);

            cmd.Parameters.AddWithValue("@LanguageId", objRequestInfo.LanguageId);
            cmd.Parameters.AddWithValue("@MobileNo", objRequestInfo.MobileNo);
            cmd.Parameters.AddWithValue("@MobileHash", objRequestInfo.MobileHash);
            cmd.Parameters.AddWithValue("@MobileEncrypted", objRequestInfo.MobileEncrypted);
            cmd.Parameters.AddWithValue("@EmailId", objRequestInfo.EmailId);
            cmd.Parameters.AddWithValue("@EmailHash", objRequestInfo.EmailHash);
            cmd.Parameters.AddWithValue("@EmailEncrypted", objRequestInfo.EmailEncrypted);
            cmd.Parameters.AddWithValue("@AccountStatusId", objRequestInfo.AccountStatusId);
            cmd.Parameters.AddWithValue("@UserRoleId", objRequestInfo.UserRoleId);
            cmd.Parameters.AddWithValue("@CreatorUserId", objRequestInfo.CreatorUserId);
            cmd.Parameters.AddWithValue("@CountryId", objRequestInfo.CountryId);
            cmd.Parameters.AddWithValue("@UserPublicId", objRequestInfo.UserPublicId);

            var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            var dbResponse = await _dbHelper.GetDBResponse(result);

            long? returnedUserId = userIdParam.Value != DBNull.Value
                ? Convert.ToInt64(userIdParam.Value)
                : null;

            return new UserAccountResponseInfo
            {
                Status = dbResponse.Status,
                Message = dbResponse.Message,
                UserId = returnedUserId
            };
        }
    }
}
