using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Constants
{
    public static class DBConstants
    {
        //Admin
        public const string sp_ValidateContentModeration = "[FeatherMomentsDB].[dbo].[sp_ValidateContentModeration]";

        //Account
        public const string usp_CreateUserAccount = "[FeatherMomentsDB].[dbo].[usp_CreateUserAccount]";

        // Auth
        public const string sp_SendOtp = "dbo.sp_SendOtp";
        public const string sp_VerifyOtp = "dbo.sp_VerifyOtp";

        // User
        public const string sp_GetUserProfile = "dbo.sp_GetUserProfile";
        public const string sp_UpdateUserProfile = "dbo.sp_UpdateUserProfile";
        public const string sp_DeleteUserAccount = "dbo.sp_DeleteUserAccount";

        // Terms
        public const string sp_GetCurrentTerms = "[FeatherMomentsDB].[dbo].[sp_GetCurrentTerms]";
        public const string sp_CheckUserTermsStatus = "[FeatherMomentsDB].[dbo].[sp_CheckUserTermsStatus]";
        public const string sp_SaveUserTermsAcceptance = "[FeatherMomentsDB].[dbo].[sp_SaveUserTermsAcceptance]";
        public const string sp_ValidateUserTerms = "[FeatherMomentsDB].[dbo].[sp_ValidateUserTerms]";

        // Property
        public const string sp_GetMyProperties = "dbo.sp_GetMyProperties";
        public const string sp_SaveProperty = "dbo.sp_SaveProperty";
        public const string sp_GetPropertyDetails = "dbo.sp_GetPropertyDetails";

        // Unit
        public const string USP_PropertyUnit_GetAll = "dbo.USP_PropertyUnit_GetAll";
        public const string USP_PropertyUnit_Save = "dbo.USP_PropertyUnit_Save";
        public const string USP_PropertyUnit_GetById = "dbo.USP_PropertyUnit_GetById";
        public const string USP_PropertyUnit_GetVacantUnits = "dbo.USP_PropertyUnit_GetVacantUnits";

        // Tenant
        public const string USP_PropertyTenant_GetAll = "dbo.USP_PropertyTenant_GetAll";
        public const string USP_PropertyTenant_Save = "dbo.USP_PropertyTenant_Save";
        public const string USP_PropertyTenant_GetById = "dbo.USP_PropertyTenant_GetById";
        public const string sp_SaveTenantAssignment = "dbo.sp_SaveTenantAssignment";
        public const string usp_GetTenantAssignment = "dbo.usp_GetTenantAssignment";
        public const string usp_SearchTenant = "dbo.usp_SearchTenant";

        // Notification
        public const string sp_SaveUserToken = "dbo.sp_SaveUserToken";
        public const string sp_GetUserNotifications = "dbo.sp_GetUserNotifications";
        public const string sp_SaveUserNotificationReadFlag = "dbo.sp_SaveUserNotificationReadFlag";

        // Master
        public const string sp_Mst_GetPropertyTypes = "dbo.sp_Mst_GetPropertyTypes";
        public const string sp_Mst_GetCountries = "[FeatherMomentsDB].[dbo].[sp_Mst_GetCountries]";
        public const string sp_Mst_GetStatesByCountryId = "[FeatherMomentsDB].[dbo].[sp_Mst_GetStatesByCountryId]";
        public const string sp_Mst_GetCitiesByStateId = "[FeatherMomentsDB].[dbo].[sp_Mst_GetCitiesByStateId]";
        public const string sp_Mst_GetUnitTypes = "dbo.sp_Mst_GetUnitTypes";
        public const string sp_Mst_GetBHKTypes = "dbo.sp_Mst_GetBHKTypes";
        public const string sp_Mst_GetFurnishingTypes = "dbo.sp_Mst_GetFurnishingTypes";
        public const string sp_Mst_GetUnitStatusTypes = "dbo.sp_Mst_GetUnitStatusTypes";
        public const string sp_Mst_GetPropertyStatusTypes = "dbo.sp_Mst_GetPropertyStatusTypes";
        public const string sp_Mst_GetAgreementStatusTypes = "dbo.sp_Mst_GetAgreementStatusTypes";
        public const string sp_Mst_GetTenantAssignmentStatusTypes = "dbo.sp_Mst_GetTenantAssignmentStatusTypes";

        // Rent
        public const string usp_SaveRentAgreement = "dbo.usp_SaveRentAgreement";
        public const string usp_GetRentAgreement = "dbo.usp_GetRentAgreement";
    }
}
