using System;
using System.ComponentModel.DataAnnotations;

namespace FEMOS.Rentora.Domain.Requests
{
    /// <summary>
    /// Request DTO for reversing a rent payment.
    /// </summary>
    public class ReverseRentPaymentRequest
    {
        [Required(ErrorMessage = "Transaction GUID is required.")]
        public Guid TransactionGuid { get; set; }

        [Required(ErrorMessage = "Reverse reason is required.")]
        [StringLength(500, ErrorMessage = "Reverse reason cannot exceed 500 characters.")]
        public string ReverseReason { get; set; }
    }
}
