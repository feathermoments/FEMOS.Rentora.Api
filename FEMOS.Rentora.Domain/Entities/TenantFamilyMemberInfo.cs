using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class TenantFamilyMemberInfo : MemberInfo
    {
        public Guid? RentAgreementPublicId { get; set; }
        public Guid? FamilyMemberPublicId { get; set; }
        public int TenantFamilyRelationId { get; set; }
        public string RelationName { get; set; }
        public bool IsPrimaryContact { get; set; }
    }
}
