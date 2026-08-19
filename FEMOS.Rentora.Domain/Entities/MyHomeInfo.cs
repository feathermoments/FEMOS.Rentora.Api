using FEMOS.Rentora.Domain.Enums;

namespace FEMOS.Rentora.Domain.Entities
{
    public class MyHomeInfo
    {
        public long PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyType { get; set; }

        public string UnitNumber { get; set; }
        public string BHKType { get; set; }
        public int FloorNo { get; set; }
        public decimal AreaSqFt { get; set; }
        public DateTime AvailableFrom { get; set; }

        public DateTime MoveInDate { get; set; }
        public DateTime ExpectedMoveOutDate { get; set; }
        public DateTime ActualMoveOutDate { get; set; }
        public string TenantAssignmentStatus { get; set; }

        public PropertyMemberInfo objOwnerInfo { get; set; }
    }
}