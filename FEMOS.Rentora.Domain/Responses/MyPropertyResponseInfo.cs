using FEMOS.Rentora.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Responses
{
    public class MyPropertyResponseInfo : BaseResponseInfo
    {
        public List<MyPropertyInfo> objProperties { get; set; } = new List<MyPropertyInfo>();
    }
}
