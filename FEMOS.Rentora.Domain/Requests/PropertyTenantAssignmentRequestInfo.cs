using FEMOS.Rentora.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Requests
{
    public class PropertyTenantAssignmentRequestInfo : BaseRequestInfo
    {
        public TenantAssignmentInfo objTenantAssignmentInfo { get; set; } = new TenantAssignmentInfo();
    }
}
