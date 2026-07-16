using System;

namespace FEMOS.Rentora.Domain.Entities
{
    public class UserPropertyInfo
    {
        // Primary
        public long PropertyId { get; set; }
        public long OwnerUserId { get; set; }
        public string PropertyCode { get; set; }
        public string PropertyName { get; set; } = string.Empty;
        public int PropertyTypeId { get; set; }

        // Description and Address
        public string Description { get; set; }
        public string AddressLine1 { get; set; } = string.Empty;
        public string AddressLine2 { get; set; }
        public string Landmark { get; set; }

        // Location Details
        public long CityId { get; set; }
        public long StateId { get; set; }
        public long CountryId { get; set; }
        public string Pincode { get; set; } = string.Empty;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        // Property Specifications
        public int TotalFloors { get; set; }
        public int TotalUnits { get; set; }
        public int TotalParkingSlots { get; set; }
        public short BuiltYear { get; set; }

        // Flags
        public bool IsVerified { get; set; } = false;
        public bool IsPublicListing { get; set; } = false;
        public bool AllowPreBooking { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }
}
