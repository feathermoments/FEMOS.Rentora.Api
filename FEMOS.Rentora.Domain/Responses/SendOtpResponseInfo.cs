using FEMOS.Rentora.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Responses
{
    public class SendOtpResponseInfo : BaseResponseInfo
    {
        public bool isExistingUser { get; set; }
    }
}
