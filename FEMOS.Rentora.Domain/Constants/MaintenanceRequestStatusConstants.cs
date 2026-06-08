using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Constants
{
    public static class MaintenanceRequestStatusConstants
    {
        public const string Open = "Open";

        public const string Assigned = "Assigned";

        public const string InProgress = "InProgress";

        public const string Completed = "Completed";

        public const string Cancelled = "Cancelled";
    }
}
