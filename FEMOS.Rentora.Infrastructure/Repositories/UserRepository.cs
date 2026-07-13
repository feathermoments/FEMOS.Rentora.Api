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
    internal class UserRepository : IUserRepository
    {
        private readonly IDBHelper _dbHelper;

        public UserRepository(IDBHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        /// <summary>
        /// Calls dbo.sp_GetUserProfileFull which must return 3 result sets:
        ///   #0 – profile row  (UserPublicId, Name, Email, MobileNumber, ProfilePhoto)
        ///   #1 – workspaces   (WorkspaceId, Name, Role)
        ///   #2 – stats row    (Votes, Polls)
        /// </summary>
        public async Task<UserProfileResponseInfo?> GetUserProfileAsync(Guid userPublicId)
        {
            var cmd = new SqlCommand(DBConstants.sp_GetUserProfile);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);

            var ds = await _dbHelper.GetDataSetBySQLCommandAsync(cmd);

            // result set 0 — core profile
            if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return null;

            var row = ds.Tables[0].Rows[0];

            var response = new UserProfileResponseInfo
            {
                UserPublicId = row.Table.Columns.Contains("UserPublicId") ? (Guid)row["UserPublicId"] : Guid.Empty,
                Name = row.Table.Columns.Contains("Name") ? row["Name"]?.ToString() ?? string.Empty : string.Empty,
                ProfilePhoto = row.Table.Columns.Contains("ProfilePhoto") ? row["ProfilePhoto"]?.ToString() ?? string.Empty : string.Empty,
                EmailAddress = row.Table.Columns.Contains("EmailAddress") ? row["EmailAddress"]?.ToString() ?? string.Empty : string.Empty,
                MobileNumber = row.Table.Columns.Contains("MobileNumber") ? row["MobileNumber"]?.ToString() ?? string.Empty : string.Empty
            };

            return response;
        }

        public async Task<DBResponseInfo> UpdateUserProfileAsync(UserProfileInfo model)
        {
            var cmd = new SqlCommand(DBConstants.sp_UpdateUserProfile);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserPublicId", model.UserPublicId);
            cmd.Parameters.AddWithValue("@Name", (object?)model.Name ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@EmailHash", (object?)model.EmailHash);
            cmd.Parameters.AddWithValue("@MobileHash", (object?)model.MobileHash);
            cmd.Parameters.AddWithValue("@EmailEncrypted", (object?)model.EmailEncrypted);
            cmd.Parameters.AddWithValue("@MobileEncrypted", (object?)model.MobileEncrypted);
            cmd.Parameters.AddWithValue("@ProfilePhoto", (object?)model.ProfilePhoto ?? DBNull.Value);

            var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            var dbResponse = await _dbHelper.GetDBResponse(result);

            return dbResponse;
        }

        public async Task<DBResponseInfo> DeleteUserAccountAsync(Guid userPublicId)
        {
            var cmd = new SqlCommand(DBConstants.sp_DeleteUserAccount);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);

            var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            var dbResponse = await _dbHelper.GetDBResponse(result);
            return dbResponse;
        }
    }
}
