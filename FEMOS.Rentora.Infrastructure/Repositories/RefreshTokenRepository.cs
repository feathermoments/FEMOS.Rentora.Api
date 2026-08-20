using FEMOS.Rentora.Domain.Constants;
using FEMOS.Rentora.Domain.Entities;
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
    internal class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IDBHelper _dbHelper;

        public RefreshTokenRepository(IDBHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<RefreshTokenInfo> ValidateRefreshTokenAsync(Guid userPublicId, string tokenHash)
        {
            var cmd = new SqlCommand(DBConstants.USP_RefreshToken_Validate);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);
            cmd.Parameters.AddWithValue("@TokenHash", tokenHash);

            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            if (dt.Rows.Count == 0)
                return null;

            var row = dt.Rows[0];
            return new RefreshTokenInfo
            {
                RefreshTokenId = Convert.ToInt64(row["RefreshTokenId"]),
                UserId = Convert.ToInt64(row["UserId"]),
                UserPublicId = (Guid)row["UserPublicId"],
                ExpiresOn = Convert.ToDateTime(row["ExpiresOn"]),
                RevokedOn = row["RevokedOn"] == DBNull.Value ? null : (DateTime?)row["RevokedOn"],
                IsActive = Convert.ToBoolean(row["IsActive"])
            };
        }

        public async Task<long> CreateRefreshTokenAsync(long userId, Guid userPublicId, string tokenHash, DateTime expiresOn, string createdByIp = null)
        {
            var cmd = new SqlCommand(DBConstants.USP_RefreshToken_Create);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);
            cmd.Parameters.AddWithValue("@TokenHash", tokenHash);
            cmd.Parameters.AddWithValue("@ExpiresOn", expiresOn);
            if (!string.IsNullOrEmpty(createdByIp))
                cmd.Parameters.AddWithValue("@CreatedByIp", createdByIp);
            else
                cmd.Parameters.AddWithValue("@CreatedByIp", DBNull.Value);

            var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            return long.TryParse(result, out var tokenId) ? tokenId : 0;
        }

        public async Task RevokeRefreshTokenAsync(Guid userPublicId, string tokenHash, string revokedReason = null)
        {
            var cmd = new SqlCommand(DBConstants.USP_RefreshToken_Revoke);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);
            cmd.Parameters.AddWithValue("@TokenHash", tokenHash);
            if (!string.IsNullOrEmpty(revokedReason))
                cmd.Parameters.AddWithValue("@RevokedReason", revokedReason);
            else
                cmd.Parameters.AddWithValue("@RevokedReason", DBNull.Value);

            await _dbHelper.ExecuteNonQueryBySQLCommandAsync(cmd);
        }

        public async Task RevokeAllRefreshTokensAsync(Guid userPublicId, string revokedReason = null)
        {
            var cmd = new SqlCommand(DBConstants.USP_RefreshToken_RevokeAll);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);
            if (!string.IsNullOrEmpty(revokedReason))
                cmd.Parameters.AddWithValue("@RevokedReason", revokedReason);
            else
                cmd.Parameters.AddWithValue("@RevokedReason", DBNull.Value);

            await _dbHelper.ExecuteNonQueryBySQLCommandAsync(cmd);
        }
    }
}

