using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class RentPaymentSummaryInfo
    {
         public decimal TotalAmount { get; set; }
         public decimal PaidAmount { get; set; }
         public decimal OutstandingAmount { get; set; }
         public int TotalPayments { get; set; }
         public bool IsOverdue { get; set; }
         public int DaysOverdue { get; set; }
    }
}
