using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FEMOS.Rentora.Domain.Entities
{
    public class RentInvoiceInfo
    {
        public int TotalRecords { get; set; }
        public int RentInvoiceId { get; set; }
        public int RentAgreementId { get; set; }
        public string AgreementNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int BillingYear { get; set; }
        public int BillingMonth { get; set; }
        public DateTime BillingStartDate { get; set; }
        public DateTime BillingEndDate { get; set; }
        public int BillingFrequencyId { get; set; }
        public int PropertyId { get; set; }
        public string PropertyName { get; set; }
        public int UnitId { get; set; }
        public string UnitNumber { get; set; }
        public int TenantAssignmentId { get; set; }
        public int TenantId { get; set; }
        public string TenantName { get; set; }
        public decimal RentAmount { get; set; }
        public decimal MaintenanceAmount { get; set; }
        public decimal ElectricityAmount { get; set; }
        public decimal WaterAmount { get; set; }
        public decimal PenaltyAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime GeneratedDate { get; set; }
        public int InvoiceStatusId { get; set; }
        public string InvoiceStatus { get; set; }
        public int PaymentStatusId { get; set; }
        public string PaymentStatus { get; set; }
        public bool IsOverDue { get; set; }
        public int DaysOverDue { get; set; }
    }
}
