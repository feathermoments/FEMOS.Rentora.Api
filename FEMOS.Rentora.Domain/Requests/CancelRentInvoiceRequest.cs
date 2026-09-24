using System;
using System.ComponentModel.DataAnnotations;

namespace FEMOS.Rentora.Domain.Requests
{
    /// <summary>
    /// Request DTO for cancelling a single rent invoice.
    /// </summary>
    public class CancelRentInvoiceRequest
    {
        [Required(ErrorMessage = "Cancel reason is required.")]
        [StringLength(500, ErrorMessage = "Cancel reason cannot exceed 500 characters.")]
        public string CancelReason { get; set; }
    }
}
