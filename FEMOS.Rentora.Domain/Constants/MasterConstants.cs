using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Constants
{
    public class MasterConstants
    {
    }
    public class ModerationStatus
    {
        public const int Pending = 0;
        public const int Approved = 1;
        public const int Rejected = 2;
        public const int AutoHidden = 3;
    }
    public class CountryConstants
    {
        public const int India = 101;
    }
    public class AccountStatusConstants
    {
        public const int Active = 1;
        public const int VerificationPending = 2;
        public const int Suspended = 3;
        public const int Deleted = 4;
        public const int DeActivated = 5;
    }
    public class  LanguageConstants
    {
        public const string English = "en-US";
    }
    public class UserRoleConstants
    {
        public const int Admin = 1;
        public const int User = 2;
    }
}
