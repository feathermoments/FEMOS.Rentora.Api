using FEMOS.Rentora.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Responses
{
    public class RentInvoiceResponseInfo : BaseResponseInfo
    {
        public RentInvoiceInfo objRentInvoiceInfo { get; set; }
        public RentAgreementInfo objRentAgreementInfo { get; set; }
        public PropertyUnitInfo objPropertyUnitInfo { get; set; }
        public PropertyMemberInfo objPropertyOwnerInfo { get; set; }
        public PropertyMemberInfo objPropertyTenantInfo { get; set; }
        public RentPaymentInfo obRentPaymentInfo { get; set; }
        public RentPaymentSummaryInfo objRentPaymentSummaryInfo { get; set; }
    }
}
