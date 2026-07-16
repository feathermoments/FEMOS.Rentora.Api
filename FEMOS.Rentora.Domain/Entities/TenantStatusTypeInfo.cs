using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class TenantAssignmentStatusTypeInfo
    {
        public int TenantAssignmentStatusId { get; set; }
        public string TenantAssignmentStatus { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }
    }
}
