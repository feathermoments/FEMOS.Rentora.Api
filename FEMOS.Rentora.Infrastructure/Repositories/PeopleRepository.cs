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
    public class PeopleRepository : IPeopleRepository
    {
        private readonly IDBHelper _dbHelper;

        public PeopleRepository(IDBHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<PropertyMembersResponseInfo> GetPropertyMembersAsync(Guid propertyPublicId, Guid userPublicId)
        {
            var response = new PropertyMembersResponseInfo();

            var cmd = new SqlCommand(DBConstants.usp_PropertyMembers_Get);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PropertyPublicId", propertyPublicId);
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);

            try
            {
                var ds = await _dbHelper.GetDataSetBySQLCommandAsync(cmd);

                // Result set 1: Property members data
                if (ds.Tables.Count > 0)
                {
                    response.objPropertyMembers = _dbHelper.ConvertDataTable<PropertyMemberInfo>(ds.Tables[0]);
                }

                // Result set 2: Status and Message
                if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    var statusRow = ds.Tables[1].Rows[0];
                    response.Status = statusRow["Status"]?.ToString() ?? StatusConstants.Success;
                    response.Message = statusRow["Message"]?.ToString() ?? "Property members retrieved successfully.";
                }
                else
                {
                    response.Status = StatusConstants.Success;
                    response.Message = "Property members retrieved successfully.";
                }
            }
            catch (Exception)
            {
                response.Status = StatusConstants.Failure;
                response.Message = "Error retrieving property members.";
            }

            return response;
        }

        public async Task<PropertyOwnersResponseInfo> GetPropertyOwnersAsync(Guid propertyPublicId, Guid userPublicId)
        {
            var response = new PropertyOwnersResponseInfo();

            var cmd = new SqlCommand(DBConstants.usp_PropertyOwners_Get);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PropertyPublicId", propertyPublicId);
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);

            try
            {
                var ds = await _dbHelper.GetDataSetBySQLCommandAsync(cmd);

                // Result set 1: Property owners data
                if (ds.Tables.Count > 0)
                {
                    response.objPropertyOwners = _dbHelper.ConvertDataTable<PropertyOwnerInfo>(ds.Tables[0]);
                }

                // Result set 2: Status and Message
                if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    var statusRow = ds.Tables[1].Rows[0];
                    response.Status = statusRow["Status"]?.ToString() ?? StatusConstants.Success;
                    response.Message = statusRow["Message"]?.ToString() ?? "Property owners retrieved successfully.";
                }
                else
                {
                    response.Status = StatusConstants.Success;
                    response.Message = "Property owners retrieved successfully.";
                }
            }
            catch (Exception)
            {
                response.Status = StatusConstants.Failure;
                response.Message = "Error retrieving property owners.";
            }

            return response;
        }

        public async Task<BaseResponseInfo> AddPropertyCoOwnerAsync(PropertyCoOwnerRequestInfo objRequestInfo)
        {
            var cmd = new SqlCommand(DBConstants.usp_PropertyCoOwner_Add);
            cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@PropertyOwnerPublicId", objRequestInfo.objPropertyOwnerInfo.PropertyOwnerPublicId);
			cmd.Parameters.AddWithValue("@PropertyPublicId", objRequestInfo.objPropertyOwnerInfo.PropertyPublicId);
            cmd.Parameters.AddWithValue("@MemberUserPublicId", objRequestInfo.objPropertyOwnerInfo.MemberUserPublicId);
			cmd.Parameters.AddWithValue("@FullName", objRequestInfo.objPropertyOwnerInfo.FullName);
            cmd.Parameters.AddWithValue("@EmailHash", objRequestInfo.objPropertyOwnerInfo.EmailHash);
            cmd.Parameters.AddWithValue("@EmailEncrypted", objRequestInfo.objPropertyOwnerInfo.EmailEncrypted);
            cmd.Parameters.AddWithValue("@MobileHash", objRequestInfo.objPropertyOwnerInfo.MobileHash);
            cmd.Parameters.AddWithValue("@MobileEncrypted", objRequestInfo.objPropertyOwnerInfo.MobileEncrypted);
            cmd.Parameters.AddWithValue("@GenderId", objRequestInfo.objPropertyOwnerInfo.GenderId);
            cmd.Parameters.AddWithValue("@DateOfBirth", objRequestInfo.objPropertyOwnerInfo.DateOfBirth);
            cmd.Parameters.AddWithValue("@ProfilePhoto", objRequestInfo.objPropertyOwnerInfo.ProfilePhoto);
            cmd.Parameters.AddWithValue("@OwnershipPercentage", objRequestInfo.objPropertyOwnerInfo.OwnershipPercentage);
            cmd.Parameters.AddWithValue("@OwnershipStartDate", objRequestInfo.objPropertyOwnerInfo.OwnershipStartDate);
            cmd.Parameters.AddWithValue("@OwnershipEndDate", objRequestInfo.objPropertyOwnerInfo.OwnershipEndDate);
            cmd.Parameters.AddWithValue("@UserPublicId", objRequestInfo.UserPublicId);

            var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            var dbResponse = await _dbHelper.GetDBResponse(result);

            return new BaseResponseInfo
            {
                Status = dbResponse.Status,
                Message = dbResponse.Message
            };
        }

        public async Task<BaseResponseInfo> UpdatePropertyCoOwnerAsync(UpdatePropertyCoOwnerRequestInfo objRequestInfo)
        {
            var cmd = new SqlCommand(DBConstants.usp_PropertyCoOwner_Update);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PropertyOwnerPublicId", objRequestInfo.objPropertyOwnerInfo.PropertyOwnerPublicId);
            cmd.Parameters.AddWithValue("@PropertyPublicId", objRequestInfo.objPropertyOwnerInfo.PropertyPublicId);
            cmd.Parameters.AddWithValue("@MemberUserPublicId", objRequestInfo.objPropertyOwnerInfo.MemberUserPublicId);
            cmd.Parameters.AddWithValue("@OwnershipPercentage", objRequestInfo.objPropertyOwnerInfo.OwnershipPercentage);
            cmd.Parameters.AddWithValue("@OwnershipStartDate", objRequestInfo.objPropertyOwnerInfo.OwnershipStartDate);
            cmd.Parameters.AddWithValue("@OwnershipEndDate", objRequestInfo.objPropertyOwnerInfo.OwnershipEndDate);
            cmd.Parameters.AddWithValue("@UserPublicId", objRequestInfo.UserPublicId);

            var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            var dbResponse = await _dbHelper.GetDBResponse(result);

            return new BaseResponseInfo
            {
                Status = dbResponse.Status,
                Message = dbResponse.Message
            };
        }

        public async Task<BaseResponseInfo> RemovePropertyCoOwnerAsync(Guid propertyPublicId, Guid propertyOwnerPublicId, Guid userPublicId)
        {
            var cmd = new SqlCommand(DBConstants.usp_PropertyCoOwner_Remove);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PropertyPublicId", propertyPublicId);
            cmd.Parameters.AddWithValue("@PropertyOwnerPublicId", propertyOwnerPublicId);
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);

            var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            var dbResponse = await _dbHelper.GetDBResponse(result);

            return new BaseResponseInfo
            {
                Status = dbResponse.Status,
                Message = dbResponse.Message
            };
        }

        public async Task<TenantFamilyMembersResponseInfo> GetTenantFamilyMembersAsync(Guid rentAgreementPublicId, Guid userPublicId)
        {
            var response = new TenantFamilyMembersResponseInfo();

            var cmd = new SqlCommand(DBConstants.usp_TenantFamilyMembers_Get);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RentAgreementPublicId", rentAgreementPublicId);
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);

            try
            {
                var ds = await _dbHelper.GetDataSetBySQLCommandAsync(cmd);

                // Result set 1: Tenant family members data
                if (ds.Tables.Count > 0)
                {
                    response.Data = _dbHelper.ConvertDataTable<TenantFamilyMemberInfo>(ds.Tables[0]);
                }

                // Result set 2: Status and Message
                if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    var statusRow = ds.Tables[1].Rows[0];
                    response.Status = statusRow["Status"]?.ToString() ?? StatusConstants.Success;
                    response.Message = statusRow["Message"]?.ToString() ?? "Tenant family members retrieved successfully.";
                }
                else
                {
                    response.Status = StatusConstants.Success;
                    response.Message = "Tenant family members retrieved successfully.";
                }
            }
            catch (Exception)
            {
                response.Status = StatusConstants.Failure;
                response.Message = "Error retrieving tenant family members.";
            }

            return response;
        }

        public async Task<BaseResponseInfo> SaveTenantFamilyMemberAsync(TenantFamilyMemberRequestInfo objRequestInfo)
        {
            var cmd = new SqlCommand(DBConstants.usp_TenantFamilyMember_Save);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RentAgreementPublicId", objRequestInfo.objTenantFamilyMemberInfo.RentAgreementPublicId);
            cmd.Parameters.AddWithValue("@FamilyMemberPublicId", objRequestInfo.objTenantFamilyMemberInfo.FamilyMemberPublicId.HasValue ? (object)objRequestInfo.objTenantFamilyMemberInfo.FamilyMemberPublicId.Value : DBNull.Value);
            cmd.Parameters.AddWithValue("@TenantFamilyRelationId", objRequestInfo.objTenantFamilyMemberInfo.TenantFamilyRelationId);
            cmd.Parameters.AddWithValue("@MemberUserPublicId", objRequestInfo.objTenantFamilyMemberInfo.MemberUserPublicId);
			cmd.Parameters.AddWithValue("@FullName", objRequestInfo.objTenantFamilyMemberInfo.FullName);
            cmd.Parameters.AddWithValue("@EmailHash", objRequestInfo.objTenantFamilyMemberInfo.EmailHash);
            cmd.Parameters.AddWithValue("@EmailEncrypted", objRequestInfo.objTenantFamilyMemberInfo.EmailEncrypted);
            cmd.Parameters.AddWithValue("@MobileHash", objRequestInfo.objTenantFamilyMemberInfo.MobileHash);
            cmd.Parameters.AddWithValue("@MobileEncrypted", objRequestInfo.objTenantFamilyMemberInfo.MobileEncrypted);
            cmd.Parameters.AddWithValue("@GenderId", objRequestInfo.objTenantFamilyMemberInfo.GenderId);
            cmd.Parameters.AddWithValue("@DateOfBirth", objRequestInfo.objTenantFamilyMemberInfo.DateOfBirth);
            cmd.Parameters.AddWithValue("@ProfilePhoto", objRequestInfo.objTenantFamilyMemberInfo.ProfilePhoto);
            cmd.Parameters.AddWithValue("@IsPrimaryContact", objRequestInfo.objTenantFamilyMemberInfo.IsPrimaryContact);
			cmd.Parameters.AddWithValue("@UserPublicId", objRequestInfo.UserPublicId);

			var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            var dbResponse = await _dbHelper.GetDBResponse(result);

            return new BaseResponseInfo
            {
                Status = dbResponse.Status,
                Message = dbResponse.Message
            };
        }

        public async Task<BaseResponseInfo> RemoveTenantFamilyMemberAsync(Guid rentAgreementPublicId, Guid familyMemberPublicId, Guid userPublicId)
        {
            var cmd = new SqlCommand(DBConstants.usp_TenantFamilyMember_Remove);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RentAgreementPublicId", rentAgreementPublicId);
            cmd.Parameters.AddWithValue("@FamilyMemberPublicId", familyMemberPublicId);
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);

            var result = await _dbHelper.ExecuteScalarBySQLCommand(cmd);
            var dbResponse = await _dbHelper.GetDBResponse(result);

            return new BaseResponseInfo
            {
                Status = dbResponse.Status,
                Message = dbResponse.Message
            };
        }

        public async Task<List<MemberUserInfo>> SearchUserAsync(Guid userPublicId, string searchText, string searchTextHash)
        {
            var response = new SearchUserResponseInfo();

            var cmd = new SqlCommand(DBConstants.usp_Search_User);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SearchText", searchText);
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);
            cmd.Parameters.AddWithValue("@SearchTextHash", searchTextHash);

            try
            {
                var dt = await _dbHelper.GetDataTableBySQLCommandAsync(cmd);
                List<MemberUserInfo> objMembers = _dbHelper.ConvertDataTable<MemberUserInfo>(dt);
                return objMembers;
            }
            catch (Exception)
            {
                response.Status = StatusConstants.Failure;
                response.Message = "Error searching for user.";
            }

            return new List<MemberUserInfo>();
        }
    }
}
