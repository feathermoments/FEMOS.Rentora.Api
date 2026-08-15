using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class RentAgreementTerminationRequestInfo
    {
        public long TerminationRequestId { get; set; }
        public Guid UniqueId { get; set; }
        public long RentAgreementId { get; set; }
        public int TerminationRequestStatusId { get; set; }
        public int RequestedByUserId { get; set; }
        public DateTime RequestedOn { get; set; }
        public DateTime TerminationDate { get; set; }
        public string Reason { get; set; }
        public int? ActionedByUserId { get; set; }
        public DateTime? ActionedOn { get; set; }
        public string ActionRemarks { get; set; }
        public DateTime? CompletedOn { get; set; }
        public string Notes { get; set; }
    }
}
