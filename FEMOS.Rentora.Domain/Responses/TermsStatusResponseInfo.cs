using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Responses
{
    public class TermsStatusResponseInfo
    {
        public int CurrentVersion { get; set; }
        public int UserVersion { get; set; }
        public bool IsAcceptanceRequired { get; set; }
    }
}
