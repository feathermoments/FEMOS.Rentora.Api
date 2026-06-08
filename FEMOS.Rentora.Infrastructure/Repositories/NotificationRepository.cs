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
    internal class NotificationRepository : INotificationRepository
    {
        private readonly IDBHelper _dbHelper;
        public NotificationRepository(IDBHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public async Task<DBResponseInfo> SaveUserToken(UserTokenRequestInfo objRequestInfo)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(DBConstants.sp_SaveUserToken);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserPublicId", objRequestInfo.UserPublicId);
                cmd.Parameters.AddWithValue("@Token", objRequestInfo.objUserTokenInfo.Token);
                cmd.Parameters.AddWithValue("@DeviceTypeId", objRequestInfo.objUserTokenInfo.DeviceTypeId);
                cmd.Parameters.AddWithValue("@DeviceName", objRequestInfo.objUserTokenInfo.DeviceName);
                cmd.Parameters.AddWithValue("@AppVersion", objRequestInfo.objUserTokenInfo.AppVersion);
                string result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
                DBResponseInfo dbResponse = await _dbHelper.GetDBResponse(result);
                return dbResponse;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<NotificationInfo>> GetUserNotificationsAsync(Guid userPublicId)
        {
            var cmd = new SqlCommand(DBConstants.sp_GetUserNotifications);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);

            var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
            return _dbHelper.ConvertDataTable<NotificationInfo>(dt);
        }

        public async Task<DBResponseInfo> SaveUserNotificationReadFlagAsync(Guid userPublicId, long notificationId)
        {
            var cmd = new SqlCommand(DBConstants.sp_SaveUserNotificationReadFlag);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);
            cmd.Parameters.AddWithValue("@NotificationId", notificationId);

            var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            var dbResponse = await _dbHelper.GetDBResponse(result);
            return dbResponse;
        }
    }
}
