using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class PropertyTenantInfo : MemberUserInfo
    {
        public long TenantId { get; set; }
        public Guid PropertyPublicId { get; set; }
        public Guid UnitPublicId { get; set; }
        public Guid TenantAssignmentPublicId { get; set; }
        public long? TenantUserId { get; set; }
        public string TenantCode { get; set; } = string.Empty;
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
