using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Constants
{
    public static class InvoiceStatusConstants
    {
        public const string Draft = "Draft";

        public const string Generated = "Generated";

        public const string Paid = "Paid";

        public const string Overdue = "Overdue";

        public const string Cancelled = "Cancelled";
    }
}
