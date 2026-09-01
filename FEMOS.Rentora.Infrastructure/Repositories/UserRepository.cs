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
            List<UserProfileInfo> objUserProfiles = _dbHelper.ConvertDataTable<UserProfileInfo>(ds.Tables[0]);
            if(objUserProfiles == null || objUserProfiles.Count == 0)
                return null;
            var objUserProfileInfo = objUserProfiles[0];
            var response = new UserProfileResponseInfo
            {
                UserPublicId = objUserProfileInfo.UserPublicId,
                Name = objUserProfileInfo.Name,
                ProfilePhoto = objUserProfileInfo.ProfilePhoto,
                EmailAddress = objUserProfileInfo.EmailAddress,
                MobileNumber = objUserProfileInfo.MobileNumber,
                GenderId = objUserProfileInfo.GenderId,
                DateOfBirth = objUserProfileInfo.DateOfBirth,
                Gender = objUserProfileInfo.Gender,
            };

            return response;
        }

        public async Task<DBResponseInfo> UpdateUserProfileAsync(UserProfileInfo model)
        {
            var cmd = new SqlCommand(DBConstants.sp_SaveUserProfile);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserPublicId", model.UserPublicId);
            cmd.Parameters.AddWithValue("@Name", (object?)model.Name ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ProfilePhoto", (object?)model.ProfilePhoto ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@GenderId", (object?)model.GenderId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DateOfBirth", (object?)model.DateOfBirth ?? DBNull.Value);

            var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            var dbResponse = await _dbHelper.GetDBResponse(result);

            return dbResponse;
        }
    }
}
