using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class RentPaymentInfo
    {
        public long RentPaymentId { get; set; }
        public string ReceiptNumber { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public string TransactionReferenceNo { get; set; }
        public decimal PaidAmount { get; set; }
        public string PaymentStatus { get; set; }
    }
}
