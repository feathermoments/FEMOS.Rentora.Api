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

        // Notification
        public const string sp_SaveUserToken = "dbo.sp_SaveUserToken";
        public const string sp_GetUserNotifications = "dbo.sp_GetUserNotifications";
        public const string sp_SaveUserNotificationReadFlag = "dbo.sp_SaveUserNotificationReadFlag";
    }
}
