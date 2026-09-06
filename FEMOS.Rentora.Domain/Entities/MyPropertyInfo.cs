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
        public Guid PropertyPublicId { get; set; }
        public string PropertyName { get; set; }
        public int PropertyTypeId { get; set; }
        public string PropertyType { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string AddressLine1 { get; set; }
        public int RoleId { get; set; }
        public string UserRole { get; set; }
        public string CoverImageUrl { get; set; }
        public TenantAssignmentSummaryInfo objTenantAssignmentSummaryInfo { get; set; }
        public PropertyQuickSummaryInfo objPropertyQuickSummaryInfo { get; set; }
    }

    public class TenantAssignmentSummaryInfo
    {
        public Guid PropertyPublicId { get; set; }
        public Guid TenantAssignmentPublicId { get; set; }
        public Guid UnitPublicId { get; set; }
        public string UnitNumber { get; set; }
        public Guid RentAgreementPublicId { get; set; }
        public bool RentAgreementIsActive { get; set; }
        public bool TenantAssignmentIsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public string RelationshipStatus { get; set; }
    }

    public class PropertyQuickSummaryInfo
    {
        public Guid PropertyPublicId { get; set; }
        public int Units { get; set; }
        public int Occupied { get; set; }
        public int Vacant { get; set; }
        public int RentDue { get; set; }
    }
}
