using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

        public int TotalRecords { get; set; }
        public long RentInvoiceId { get; set; }
        public long PaidByUserId { get; set; }
        public decimal PaidAmount { get; set; }
        public DateTime PaidOn { get; set; }
        public string? PaymentGatewayTransactionId { get; set; }
        public long ReceivedBy { get; set; }
        public DateTime ReceivedOn { get; set; }
        public DateTime ReversedOn { get; set; }
        public long ReversedBy { get; set; }
        public string? ReverseReason { get; set; }
        public string? AgreementNumber { get; set; }
        public long PropertyId { get; set; }
        public string? PropertyName { get; set; }
        public long UnitId { get; set; }
        public string? UnitNumber { get; set; }
        public long TenantAssignmentId { get; set; }
        public long TenantId { get; set; }
        public string? TenantName { get; set; }
        public short PaymentStatusId { get; set; }
    }

    public class RentPaymentInvoiceInfo
    {
        public long RentInvoiceId { get; set; }
        public decimal OutstandingAmount { get; set; }
        public decimal PaidAmount { get; set; }
    }
}
