using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class MyPropertyTenantInfo
    {
        public int TenantId { get; set; }
        public int PropertyId { get; set; }
        public int PropertyUnitId { get; set; }
        public string UnitNumber { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public decimal MonthlyRent { get; set; }
        public DateTime AgreementStartDate { get; set; }
        public DateTime AgreementEndDate { get; set; }
        public int TenantStatusId { get; set; }
        public string TenantStatus { get; set; } = string.Empty;
        public bool IsPrimaryTenant { get; set; }
    }
}
