using System;
using System.ComponentModel.DataAnnotations;

namespace FEMOS.Rentora.Domain.Requests
{
    /// <summary>
    /// Request DTO for cancelling future rent invoices associated with a rent agreement.
    /// </summary>
    public class CancelFutureInvoicesRequest
    {
        [Range(1, long.MaxValue, ErrorMessage = "Rent agreement ID must be greater than zero.")]
        public long RentAgreementId { get; set; }

        [Required(ErrorMessage = "Effective date is required.")]
        public DateTime EffectiveDate { get; set; }

        [Required(ErrorMessage = "Cancel reason is required.")]
        [StringLength(500, ErrorMessage = "Cancel reason cannot exceed 500 characters.")]
        public string CancelReason { get; set; }
    }
}
