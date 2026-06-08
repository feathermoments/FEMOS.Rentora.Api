using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Constants
{
    public static class PaymentStatusConstants
    {
        public const string Pending = "Pending";

        public const string Paid = "Paid";

        public const string Failed = "Failed";

        public const string Partial = "Partial";

        public const string Refunded = "Refunded";
    }
}
