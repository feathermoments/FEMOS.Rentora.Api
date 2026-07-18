using FEMOS.Rentora.Domain.Enums;

namespace FEMOS.Rentora.Domain.Entities
{
    public class PropertySummaryInfo
    {
        public long PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyType { get; set; }
        public int TotalFloors { get; set; }
        public int TotalUnits { get; set; }
        public int ConfiguredUnits { get; set; }
        public int VacantUnits { get; set; }
        public int OccupiedUnits { get; set; }
        public int ReservedUnits { get; set; }
        public int MaintenanceUnits { get; set; }
        public int BlockedUnits { get; set; }
        public int InactiveUnits { get; set; }
        public decimal TotalRentPotential { get; set; }
        public decimal OccupancyPercentage { get; set; }
    }
}
