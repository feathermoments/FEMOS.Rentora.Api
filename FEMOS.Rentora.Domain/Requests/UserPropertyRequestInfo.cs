using FEMOS.Rentora.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Requests
{
    public class UserPropertyRequestInfo : BaseRequestInfo
    {
        public UserPropertyInfo objUserPropertyInfo { get; set; } = new UserPropertyInfo();
    }
}
