using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class PropertyTenantInfo
    {
        public long TenantId { get; set; }
        public long PropertyId { get; set; }
        public long UnitId { get; set; }
        public long TenantAssignmentId { get; set; }
        public long? TenantUserId { get; set; }
        public string TenantCode { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public string MobileHash { get; set; } = string.Empty;
        public string MobileEncrypted { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string EmailHash { get; set; } = string.Empty;
        public string EmailEncrypted { get; set; } = string.Empty;
        public string GenderId { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Occupation { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string PermanentAddress { get; set; } = string.Empty;
        public string CompanyAddress { get; set; } = string.Empty;
        public string EmergencyContactName { get; set; } = string.Empty;
        public string EmergencyContactNumber { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
