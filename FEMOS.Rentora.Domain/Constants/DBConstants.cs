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
        public const string sp_GetUserPropertyRole = "dbo.sp_GetUserPropertyRole";

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
        public const string usp_DeletePropertyTenant = "dbo.usp_DeletePropertyTenant";
        public const string usp_DeleteTenantAssignment = "dbo.usp_DeleteTenantAssignment";

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
        public const string sp_Mst_GetUnitTypesByPropertyType = "dbo.sp_Mst_GetUnitTypesByPropertyType";
        public const string USP_Mst_GetPaymentMethods = "dbo.USP_Mst_GetPaymentMethods";

        // Rent
        public const string usp_SaveRentAgreement = "dbo.usp_SaveRentAgreement";
        public const string usp_GetRentAgreement = "dbo.usp_GetRentAgreement";
        public const string usp_DeleteRentAgreement = "dbo.usp_DeleteRentAgreement";
        public const string USP_RentPayment_Save = "dbo.USP_RentPayment_Save";
        public const string USP_RentInvoice_List = "dbo.USP_RentInvoice_List";
        public const string USP_RentInvoice_GetDetails = "USP_RentInvoice_GetDetails";

        //Menu
        public const string USP_GetUserMenus = "dbo.USP_GetUserMenus";
        public const string USP_GetUserMenuPermissions = "dbo.USP_GetUserMenuPermissions";

        // Dashboard
        public const string USP_Dashboard_GetWidgetsByRole = "dbo.USP_Dashboard_GetWidgetsByRole";
        public const string USP_Dashboard_PropertySummary = "dbo.USP_Dashboard_PropertySummary";
        public const string USP_Dashboard_RentSummary = "dbo.USP_Dashboard_RentSummary";
        public const string USP_Dashboard_RecentPayments = "dbo.USP_Dashboard_RecentPayments";
        public const string USP_Dashboard_OpenRequests = "dbo.USP_Dashboard_OpenRequests";
        public const string USP_Dashboard_UpcomingRenewals = "dbo.USP_Dashboard_UpcomingRenewals";
        public const string USP_Dashboard_MyHome = "dbo.USP_Dashboard_MyHome";
        public const string USP_Dashboard_Agreement = "dbo.USP_Dashboard_Agreement";
        public const string USP_Dashboard_MyRequests = "dbo.USP_Dashboard_MyRequests";
        public const string USP_Dashboard_StaffSummary = "dbo.USP_Dashboard_StaffSummary";
        public const string USP_Dashboard_ReportSummary = "dbo.USP_Dashboard_ReportSummary";
    }
}
