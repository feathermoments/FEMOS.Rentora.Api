using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class MyPropertyTenantInfo
    {
        public long TenantId { get; set; }
        public long PropertyId { get; set; }
        public long UnitId { get; set; }
        public long TenantAssignmentId { get; set; }
        public string UnitNumber { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public decimal MonthlyRent { get; set; }
        public DateTime AgreementStartDate { get; set; }
        public DateTime AgreementEndDate { get; set; }
        public int AgreementStatusId { get; set; }
        public string AgreementStatus { get; set; } = string.Empty;
        public DateTime MoveInDate { get; set; }
        public DateTime MoveOutDate { get; set; }
        public int TenantAssignmentStatusId { get; set; }
        public string TenantAssignmentStatus { get; set; } = string.Empty;
        public bool IsPrimaryTenant { get; set; }
    }
}
