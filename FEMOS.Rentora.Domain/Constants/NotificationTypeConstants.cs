using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Constants
{
    public static class NotificationTypeConstants
    {
        public const string RentReminder = "RentReminder";

        public const string PaymentReceived = "PaymentReceived";

        public const string MaintenanceUpdate = "MaintenanceUpdate";

        public const string LeaseExpiry = "LeaseExpiry";

        public const string Announcement = "Announcement";
    }
}
