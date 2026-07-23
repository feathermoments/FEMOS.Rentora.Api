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
        public decimal PaymentAmount { get; set; }
        public string PaymentStatus { get; set; }
        /// <summary>
        /// save fields for audit purpose, who paid the amount
        /// </summary>
        public short PaymentMethodId { get; set; }
        public string ReferenceNumber { get; set; }

        public string GatewayName { get; set; }

        public string GatewayTransactionId { get; set; }

        public string GatewayResponse { get; set; }

        public bool IsOnlinePayment { get; set; }

        public string Remarks { get; set; }

        public Guid TransactionGuid { get; set; }

        public List<RentPaymentInvoiceInfo> Invoices { get; set; }
    }

    public class RentPaymentInvoiceInfo
    {
        public long RentInvoiceId { get; set; }
        public decimal OutstandingAmount { get; set; }
        public decimal PaidAmount { get; set; }
    }
}
