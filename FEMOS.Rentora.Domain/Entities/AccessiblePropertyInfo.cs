using System;

namespace FEMOS.Rentora.Domain.Entities
{
    /// <summary>
    /// Represents a property accessible to a user with their internal role for that property.
    /// </summary>
    public class AccessiblePropertyInfo
    {
        public Guid PropertyUniqueId { get; set; }
        public string PropertyName { get; set; }
        public long InternalRoleId { get; set; }
        public string InternalRoleCode { get; set; }
        public string InternalRoleName { get; set; }
    }
}
