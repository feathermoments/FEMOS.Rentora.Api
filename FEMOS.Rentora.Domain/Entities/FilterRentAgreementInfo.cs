using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class FilterRentAgreementInfo : BaseFilterInfo
    {
        public long TenantId { get; set; }
    }
}
