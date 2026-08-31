using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class PropertyOwnerInfo : MemberInfo
    {
        public Guid PropertyOwnerPublicId { get; set; }
        public Guid PropertyPublicId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public decimal OwnershipPercentage { get; set; }
        public bool IsPrimaryOwner { get; set; }
        public DateTime? OwnershipStartDate { get; set; }
        public DateTime? OwnershipEndDate { get; set; }
    }
}
