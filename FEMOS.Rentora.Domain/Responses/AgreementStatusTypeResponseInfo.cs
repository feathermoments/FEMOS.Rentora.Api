using FEMOS.Rentora.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Responses
{
    public class AgreementStatusTypeResponseInfo : BaseResponseInfo
    {
        public List<AgreementStatusTypeInfo> objAgreementStatusTypes { get; set; }
    }
}
