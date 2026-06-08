using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Responses
{
    public class BaseResponseInfo
    {
        public string Status { get; set; } = "Success";
        public string Message { get; set; }
    }
}
