using FEMOS.Rentora.Application.Interfaces;
using FEMOS.Rentora.Domain.Requests;
using FEMOS.Rentora.Domain.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FEMOS.Rentora.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentController : ControllerBase
    {
        private readonly IRentService _rentService;
        public RentController(IRentService rentService)
        {
            _rentService = rentService;
        }

        [HttpPost("get-rent-invoices")]
        public async Task<IActionResult> GetRentInvoices(FilterRequestInfo objRequestInfo)
        {
            if (objRequestInfo == null)
            {
                throw new ArgumentNullException(nameof(objRequestInfo));
            }
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            objRequestInfo.UserPublicId = userPublicId;
            var result = await _rentService.GetRentInvoicesAsync(objRequestInfo);
            return Ok(result);
        }

        [HttpGet("get-rent-invoice-details/{propertyPublicId}/{rentInvoicePublicId}")]
        public async Task<IActionResult> GetRentInvoiceDetails(Guid propertyPublicId, Guid rentInvoicePublicId)
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            var rentAgreement = await _rentService.GetRentInvoiceDetailsAsync(userPublicId, propertyPublicId, rentInvoicePublicId);
            return Ok(rentAgreement);
        }

        [HttpPost("save-rent-payment")]
        public async Task<IActionResult> SaveRentPayment(RentPaymentRequestInfo objRequestInfo)
        {
            if (objRequestInfo == null)
            {
                throw new ArgumentNullException(nameof(objRequestInfo));
            }
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            objRequestInfo.UserPublicId = userPublicId;
            var result = await _rentService.SaveRentPaymentAsync(objRequestInfo);
            return Ok(result);
        }

        [HttpPost("get-rent-payments")]
        public async Task<IActionResult> GetRentPayments(FilterRequestInfo objRequestInfo)
        {
            if (objRequestInfo == null)
            {
                throw new ArgumentNullException(nameof(objRequestInfo));
            }
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            objRequestInfo.UserPublicId = userPublicId;
            var result = await _rentService.GetRentPaymentsAsync(objRequestInfo);
            return Ok(result);
        }

        [HttpPatch("save-rent-payment-action")]
        public async Task<IActionResult> UpdateRentPaymentAction(RentPaymentActionRequestInfo objRequestInfo)
        {
            if (objRequestInfo == null)
            {
                throw new ArgumentNullException(nameof(objRequestInfo));
            }
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            objRequestInfo.UserPublicId = userPublicId;
            var result = await _rentService.UpdateRentPaymentActionAsync(objRequestInfo);
            return Ok(result);
        }

        [HttpGet("get-rent-payment-details/{propertyPublicId}/{rentPaymentPublicId}")]
        public async Task<IActionResult> GetRentPaymentDetails(Guid propertyPublicId, Guid rentPaymentPublicId)
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            var rentAgreement = await _rentService.GetRentPaymentDetailsAsync(userPublicId, propertyPublicId, rentPaymentPublicId);
            return Ok(rentAgreement);
        }

        /// <summary>
        /// GET /api/rent/get-tenant-security-deposits/{propertyPublicId}
        /// Retrieves all tenant security deposits for a specific property.
        /// Response: { status, message, objTenantSecurityDeposits }
        /// </summary>
        [HttpPost("get-tenant-security-deposits")]
        public async Task<IActionResult> GetTenantSecurityDeposits(FilterRequestInfo objRequestInfo)
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();

            objRequestInfo.UserPublicId = userPublicId;

            var result = await _rentService.GetTenantSecurityDepositsAsync(objRequestInfo);
            return Ok(result);
        }

        /// <summary>
        /// GET /api/rent/get-tenant-security-deposit-details/{tenantSecurityDepositId}/{rentAgreementPublicId}/{tenantAssignmentPublicId}
        /// Retrieves details of a specific tenant security deposit.
        /// Response: { status, message, objTenantSecurityDepositInfo }
        /// </summary>
        [HttpGet("get-tenant-security-deposit-details/{tenantSecurityDepositId}/{rentAgreementPublicId}/{tenantAssignmentPublicId}")]
        public async Task<IActionResult> GetTenantSecurityDepositDetails(long tenantSecurityDepositId, Guid rentAgreementPublicId, Guid tenantAssignmentPublicId)
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();

            if (tenantSecurityDepositId <= 0 || rentAgreementPublicId == Guid.Empty || tenantAssignmentPublicId == Guid.Empty)
                return BadRequest(new { status = "Failure", message = "Invalid parameters." });

            var result = await _rentService.GetTenantSecurityDepositDetailsAsync(userPublicId, tenantSecurityDepositId, rentAgreementPublicId, tenantAssignmentPublicId);
            return Ok(result);
        }

        /// <summary>
        /// GET /api/rent/get-tenant-security-deposit-transactions/{tenantSecurityDepositId}/{rentAgreementPublicId}/{tenantAssignmentPublicId}
        /// Retrieves transaction history for a tenant security deposit.
        /// Response: { status, message, objDepositTransactions }
        /// </summary>
        [HttpGet("get-tenant-security-deposit-transactions/{tenantSecurityDepositId}/{rentAgreementPublicId}/{tenantAssignmentPublicId}")]
        public async Task<IActionResult> GetTenantSecurityDepositTransactions(long tenantSecurityDepositId, Guid rentAgreementPublicId, Guid tenantAssignmentPublicId)
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();

            if (tenantSecurityDepositId <= 0 || rentAgreementPublicId == Guid.Empty || tenantAssignmentPublicId == Guid.Empty)
                return BadRequest(new { status = "Failure", message = "Invalid parameters." });

            var result = await _rentService.GetTenantSecurityDepositTransactionsAsync(userPublicId, tenantSecurityDepositId, rentAgreementPublicId, tenantAssignmentPublicId);
            return Ok(result);
        }

        /// <summary>
        /// POST /api/rent/rent-invoices/{rentInvoicePublicId}/cancel
        /// Cancel a single rent invoice.
        /// 
        /// Request: { "cancelReason": "Duplicate invoice generated" }
        /// Response: { "status": "Success/Failure", "message": "..." }
        /// </summary>
        [HttpPost("rent-invoices/{rentInvoicePublicId}/cancel")]
        public async Task<IActionResult> CancelRentInvoice(Guid rentInvoicePublicId, [FromBody] CancelRentInvoiceRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            // Validate GUID
            if (rentInvoicePublicId == Guid.Empty)
                return BadRequest(new { Status = "Failure", Message = "Invalid rent invoice ID." });

            // Validate request
            if (string.IsNullOrWhiteSpace(request.CancelReason))
                return BadRequest(new { Status = "Failure", Message = "Cancel reason is required." });

            if (request.CancelReason.Length > 500)
                return BadRequest(new { Status = "Failure", Message = "Cancel reason cannot exceed 500 characters." });

            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();

            var result = await _rentService.CancelRentInvoiceAsync(userPublicId, rentInvoicePublicId, request.CancelReason);
            return Ok(result);
        }

        /// <summary>
        /// POST /api/rent/rent-payments/{rentPaymentId}/reverse
        /// Reverse a rent payment and reverse its financial allocations, receipt and ledger impact.
        /// 
        /// Request: { "transactionGuid": "GUID", "reverseReason": "Payment entered incorrectly" }
        /// Response: { "status": "Success/Failure", "message": "..." }
        /// </summary>
        [HttpPost("rent-payments/{rentPaymentId}/reverse")]
        public async Task<IActionResult> ReverseRentPayment(long rentPaymentId, [FromBody] ReverseRentPaymentRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            // Validate route parameter
            if (rentPaymentId <= 0)
                return BadRequest(new { Status = "Failure", Message = "Rent payment ID must be greater than zero." });

            // Validate GUID
            if (request.TransactionGuid == Guid.Empty)
                return BadRequest(new { Status = "Failure", Message = "Invalid transaction GUID." });

            // Validate request
            if (string.IsNullOrWhiteSpace(request.ReverseReason))
                return BadRequest(new { Status = "Failure", Message = "Reverse reason is required." });

            if (request.ReverseReason.Length > 500)
                return BadRequest(new { Status = "Failure", Message = "Reverse reason cannot exceed 500 characters." });

            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();

            var result = await _rentService.ReverseRentPaymentAsync(userPublicId, rentPaymentId, request.TransactionGuid, request.ReverseReason);
            return Ok(result);
        }
    }
}