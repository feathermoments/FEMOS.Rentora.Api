using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class TenantAssignmentInfo
    {
        public long TenantAssignmentId { get; set; }
        public long TenantId { get; set; }
        public long PropertyId { get; set; }
        public long PropertyUnitId { get; set; }
        public DateTime MoveInDate { get; set; }
        public DateTime ExpectedMoveOutDate { get; set; }
        public DateTime ActualMoveOutDate { get; set; }
        public int TenantStatusId { get; set; }
        public bool IsPrimaryTenant { get; set; }
        public bool IsActive { get; set; }
    }
}
