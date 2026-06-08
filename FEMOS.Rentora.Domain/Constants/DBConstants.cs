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

        // Workspace
        public const string sp_Workspace_Create = "dbo.sp_Workspace_Create";
        public const string sp_GetUserWorkspaces = "dbo.sp_GetUserWorkspaces";
        public const string sp_GetWorkspaceDetails = "dbo.sp_GetWorkspaceDetails";
        public const string sp_Workspace_Update = "dbo.sp_Workspace_Update";
        public const string sp_GetWorkspaceTypes = "dbo.sp_GetWorkspaceTypes";
        public const string sp_GetPublicWorkspaces = "dbo.sp_GetPublicWorkspaces";
        public const string sp_SearchWorkspaces = "dbo.sp_SearchWorkspaces";

        // Workspace Members
        public const string sp_InviteMember = "dbo.sp_InviteMember";
        public const string sp_JoinWorkspace = "dbo.sp_JoinWorkspace";
        public const string sp_JoinWorkspaceWithInvite = "dbo.sp_JoinWorkspaceWithInvite";
        public const string sp_ApproveMember = "dbo.sp_ApproveMember";
        public const string sp_GetWorkspaceMembers = "dbo.sp_GetWorkspaceMembers";
        public const string sp_GetWorkspaceMemberInvites = "dbo.sp_GetWorkspaceMemberInvites";
        public const string sp_RespondToWorkspaceInvite = "dbo.sp_RespondToWorkspaceInvite";
        public const string sp_SaveWorkspaceInviteCode = "dbo.sp_SaveWorkspaceInviteCode";
        public const string sp_WorkspaceValidateInviteCode = "dbo.sp_WorkspaceValidateInviteCode";
        public const string sp_GetWorkspaceInvites = "dbo.sp_GetWorkspaceInvites";
        public const string sp_WorkspaceRemoveMember = "dbo.sp_WorkspaceRemoveMember";
        public const string sp_WorkspaceExitMember = "dbo.sp_WorkspaceExitMember";

        // Terms
        public const string sp_GetCurrentTerms = "dbo.sp_GetCurrentTerms";
        public const string sp_CheckUserTermsStatus = "dbo.sp_CheckUserTermsStatus";
        public const string sp_SaveUserTermsAcceptance = "dbo.sp_SaveUserTermsAcceptance";
        public const string sp_ValidateUserTerms = "dbo.sp_ValidateUserTerms";

        // Workspace Verification Admin
        public const string sp_WorkspaceVerification_Request = "dbo.sp_WorkspaceVerification_Request";
        public const string sp_WorkspaceVerification_Approve = "dbo.sp_WorkspaceVerification_Approve";
        public const string sp_WorkspaceVerification_Reject = "dbo.sp_WorkspaceVerification_Reject";
        public const string sp_WorkspaceVerification_Revoke = "dbo.sp_WorkspaceVerification_Revoke";
        public const string sp_WorkspaceVerification_GetStatus = "dbo.sp_WorkspaceVerification_GetStatus";
        public const string sp_WorkspaceVerification_GetByRequest = "dbo.sp_WorkspaceVerification_GetByRequest";

        // Workspace Reports
        public const string sp_WorkspaceReports_GetPending = "dbo.sp_WorkspaceReports_GetPending";
        public const string sp_WorkspaceReport_Insert = "dbo.sp_WorkspaceReport_Insert";
        public const string sp_WorkspaceReports_GetByWorkspace = "dbo.sp_WorkspaceReports_GetByWorkspace";
        public const string sp_WorkspaceReport_Respond = "dbo.sp_WorkspaceReport_Respond";
        public const string sp_WorkspaceTrust_Get = "dbo.sp_WorkspaceTrust_Get";

        // Poll
        public const string sp_SavePoll = "dbo.sp_SavePoll";
        public const string sp_DeletePoll = "dbo.sp_DeletePoll";
        public const string sp_GetPolls = "dbo.sp_GetPolls";
        public const string sp_GetPollDetails = "dbo.sp_GetPollDetails";
        public const string sp_GetPollOptions = "dbo.sp_GetPollOptions";
        public const string sp_GetActivePolls = "dbo.sp_GetActivePolls";
        public const string sp_GetDashboardStats = "dbo.sp_GetDashboardStats";

        // Vote
        public const string sp_CastVote = "dbo.sp_CastVote";
        public const string sp_GetPollResults = "dbo.sp_GetPollResults";

        // Category
        public const string sp_GetCategories = "dbo.sp_GetCategories";
        public const string sp_GetPollCategories = "dbo.sp_GetPollCategories";

        // Reaction
        public const string sp_SavePollReactions = "dbo.sp_SavePollReactions";
        public const string sp_GetPollReactionCount = "dbo.sp_GetPollReactionCount";

        // Comment
        public const string sp_AddComment = "dbo.sp_AddComment";
        public const string sp_GetComments = "dbo.sp_GetComments";

        // Verification
        public const string sp_GetVerificationStatus = "dbo.sp_GetVerificationStatus";
        public const string sp_GetVerificationRequests = "dbo.sp_GetVerificationRequests";
        public const string sp_ProcessVerification = "dbo.sp_ProcessVerification";
        public const string sp_SaveUserToken = "dbo.sp_SaveUserToken";

        // Report
        public const string sp_ReportWorkspace = "dbo.sp_ReportWorkspace";
        public const string sp_WorkspaceReport_Resolve = "dbo.sp_WorkspaceReport_Resolve";

        // Poll Reports
        public const string sp_CreatePollReport = "dbo.sp_CreatePollReport";
        public const string sp_GetReportedPolls = "dbo.sp_GetReportedPolls";
        public const string sp_GetPollReportSummary = "dbo.sp_GetPollReportSummary";
        public const string sp_ResolvePollReport = "dbo.sp_ResolvePollReport";
        public const string sql_GetReportReasons = "SELECT ReportReasonId, ReasonName FROM dbo.Mst_ReportReasons WHERE IsActive = 1 ORDER BY DisplayOrder";

        // Workspace Reports (Admin)
        public const string sp_GetReportedWorkspaces = "dbo.sp_GetReportedWorkspaces";
        public const string sp_GetWorkspaceReportSummary = "dbo.sp_GetWorkspaceReportSummary";

        // Notification
        public const string sp_GetUserNotifications = "dbo.sp_GetUserNotifications";
        public const string sp_SaveUserNotificationReadFlag = "dbo.sp_SaveUserNotificationReadFlag";
    }
}
