using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class MyPropertyInfo
    {
        public int PropertyId { get; set; }
        public string PropertyName { get; set; } = string.Empty;
        public string PropertyType { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string AddressLine1 { get; set; } = string.Empty;
        public int TotalUnits { get; set; }
        public int OccupiedUnits { get; set; }
        public int VacantUnits { get; set; }
        public int RoleId { get; set; }
        public string UserRole { get; set; } = string.Empty;
    }
}
