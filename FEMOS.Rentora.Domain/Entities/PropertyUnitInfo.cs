using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class PropertyUnitInfo
    {
        public long UnitId { get; set; }
        public long PropertyId { get; set; }
        public string PropertyName { get; set; } = string.Empty;
        public int UnitNumber { get; set; }
        public int FloorNo { get; set; }
        public int UnitTypeId { get; set; }
        public string UnitTypeName { get; set; } = string.Empty;
        public int BHKTypeId { get; set; }
        public string BHKTypeName { get; set; } = string.Empty;
        public int AreaSqFt { get; set; }
        public int BedroomCount { get; set; }
        public int BathroomCount { get; set; }
        public int BalconyCount { get; set; }
        public int FurnishingTypeId { get; set; }
        public string FurnishingTypeName { get; set; } = string.Empty;
        public int MonthlyRent { get; set; }
        public int SecurityDeposit { get; set; }
        public int MaintenanceAmount { get; set; }
        public int ElectricityMeterNo { get; set; }
        public int WaterMeterNo { get; set; }
        public bool IsParkingIncluded { get; set; }
        public int UnitStatusId { get; set; }
        public string UnitStatusName { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
        public DateTime AvailableFrom { get; set; }
        //public DateTime CreatedOn { get; set; }
        //public DateTime UpdatedOn { get; set; }
    }
}
