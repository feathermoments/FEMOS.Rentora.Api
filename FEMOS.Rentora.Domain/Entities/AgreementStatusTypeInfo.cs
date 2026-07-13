using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class AgreementStatusTypeInfo
    {
            public int AgreementStatusId { get; set; }
            public string AgreementStatus { get; set; } = string.Empty;
            public string Descriptions { get; set; } = string.Empty;
    }
}
