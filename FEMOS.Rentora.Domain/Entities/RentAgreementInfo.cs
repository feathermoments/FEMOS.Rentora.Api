using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class RentAgreementInfo
    {
        public long RentAgreementId { get; set; }
        //public long? PropertyId { get; set; }
        //public long? UnitId { get; set; }
        //public long? TenantId { get; set; }
        public long TenantAssignmentId { get; set; }
        public string AgreementNumber { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal MonthlyRent { get; set; }
        public decimal SecurityDeposit { get; set; }
        public decimal MaintenanceAmount { get; set; }
        public int RentDueDay { get; set; }
        public int NoticePeriodDays { get; set; }
        public int AgreementStatusId { get; set; }
        public string AgreementStatus { get; set; } = string.Empty;
        public string AgreementDocumentUrl { get; set; } = string.Empty;
        public int BillingCycleTypeId { get; set; }
        public string? BillingCycleType { get; set; }
        public int ProrationTypeId { get; set; }
        public string? ProrationType { get; set; }
        public int CurrentRenewalNo { get; set; }
        public int BillingCycleStartDay { get; set; }
        public long PreviousRentAgreementId { get; set; }
        public bool IsActive { get; set; }
        public string? UnitNumber { get; set; } = string.Empty;  
        public string? PropertyName { get; set; } = string.Empty;
        public string? TenantName { get; set; } = string.Empty;
    }
}
