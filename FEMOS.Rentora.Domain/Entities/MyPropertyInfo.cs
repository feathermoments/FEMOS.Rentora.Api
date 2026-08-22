using FEMOS.Rentora.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class MyPropertyInfo
    {
        public long PropertyId { get; set; }
        public Guid PropertyPublicId { get; set; }
        public string PropertyName { get; set; } = string.Empty;
        public int PropertyTypeId { get; set; }
        public string PropertyType { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string AddressLine1 { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public string UserRole { get; set; } = string.Empty;
        public long CurrentUnitId { get; set; }
        public string CurrentUnitNumber { get; set; } = string.Empty;
        public string RelationshipStatus { get; set; } = string.Empty;
        public long TenantAssignmentId { get; set; }
    }
}
