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

        [HttpGet("get-rent-invoice-details/{propertyId}/{rentInvoiceId}")]
        public async Task<IActionResult> GetRentInvoiceDetails(long propertyId, long rentInvoiceId)
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            var rentAgreement = await _rentService.GetRentInvoiceDetailsAsync(userPublicId, propertyId, rentInvoiceId);
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

        [HttpGet("get-rent-payment-details/{propertyId}/{rentPaymentId}")]
        public async Task<IActionResult> GetRentPaymentDetails(long propertyId, long rentPaymentId)
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();
            var rentAgreement = await _rentService.GetRentPaymentDetailsAsync(userPublicId, propertyId, rentPaymentId);
            return Ok(rentAgreement);
        }

        /// <summary>
        /// GET /api/rent/get-tenant-security-deposits/{propertyId}
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
        /// GET /api/rent/get-tenant-security-deposit-details/{tenantSecurityDepositId}/{rentAgreementId}/{tenantAssignmentId}
        /// Retrieves details of a specific tenant security deposit.
        /// Response: { status, message, objTenantSecurityDepositInfo }
        /// </summary>
        [HttpGet("get-tenant-security-deposit-details/{tenantSecurityDepositId}/{rentAgreementId}/{tenantAssignmentId}")]
        public async Task<IActionResult> GetTenantSecurityDepositDetails(long tenantSecurityDepositId, long rentAgreementId, long tenantAssignmentId)
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();

            if (tenantSecurityDepositId <= 0 || rentAgreementId <= 0 || tenantAssignmentId <= 0)
                return BadRequest(new { status = "Failure", message = "Invalid parameters." });

            var result = await _rentService.GetTenantSecurityDepositDetailsAsync(userPublicId, tenantSecurityDepositId, rentAgreementId, tenantAssignmentId);
            return Ok(result);
        }

        /// <summary>
        /// GET /api/rent/get-tenant-security-deposit-transactions/{tenantSecurityDepositId}/{rentAgreementId}/{tenantAssignmentId}
        /// Retrieves transaction history for a tenant security deposit.
        /// Response: { status, message, objDepositTransactions }
        /// </summary>
        [HttpGet("get-tenant-security-deposit-transactions/{tenantSecurityDepositId}/{rentAgreementId}/{tenantAssignmentId}")]
        public async Task<IActionResult> GetTenantSecurityDepositTransactions(long tenantSecurityDepositId, long rentAgreementId, long tenantAssignmentId)
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();

            if (tenantSecurityDepositId <= 0 || rentAgreementId <= 0 || tenantAssignmentId <= 0)
                return BadRequest(new { status = "Failure", message = "Invalid parameters." });

            var result = await _rentService.GetTenantSecurityDepositTransactionsAsync(userPublicId, tenantSecurityDepositId, rentAgreementId, tenantAssignmentId);
            return Ok(result);
        }
    }
}