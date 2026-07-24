using FEMOS.Rentora.Domain.Entities;
using FEMOS.Rentora.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Responses
{
    public class FilterRentPaymentRequestInfo : BaseRequestInfo
    {
        public FilterRentPaymentInfo objFilterInfo { get; set; }
    }
}
