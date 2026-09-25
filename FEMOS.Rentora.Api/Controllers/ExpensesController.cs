using FEMOS.Rentora.Application.Interfaces;
using FEMOS.Rentora.Application.Interfaces;
using FEMOS.Rentora.Domain.Requests;
using FEMOS.Rentora.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpensesController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        /// <summary>
        /// Add a new expense
        /// POST /api/expenses
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddExpense(ExpenseCreateRequestInfo objRequestInfo)
        {
            if (objRequestInfo == null)
            {
                throw new ArgumentNullException(nameof(objRequestInfo));
            }

            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();

            objRequestInfo.UserPublicId = userPublicId;
            var result = await _expenseService.AddExpenseAsync(objRequestInfo);
            return Ok(result);
        }

        /// <summary>
        /// Get expense summary
        /// GET /api/expenses/summary?propertyPublicId=...&unitPublicId=...&fromDate=...&toDate=...
        /// </summary>
        [HttpGet("summary")]
        public async Task<IActionResult> GetExpenseSummary(
            [FromQuery] Guid? propertyPublicId,
            [FromQuery] Guid? unitPublicId,
            [FromQuery] DateTime? fromDate,
            [FromQuery] DateTime? toDate)
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();

            var result = await _expenseService.GetExpenseSummaryAsync(userPublicId, propertyPublicId, unitPublicId, fromDate, toDate);
            return Ok(result);
        }

        /// <summary>
        /// List expenses with optional filters
        /// POST /api/expenses/list
        /// </summary>
        [HttpPost("get-property-expenses")]
        public async Task<IActionResult> ListExpenses(FilterRequestInfo objRequestInfo)
        {
            if (objRequestInfo == null)
            {
                throw new ArgumentNullException(nameof(objRequestInfo));
            }

            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();

            objRequestInfo.UserPublicId = userPublicId;
            var result = await _expenseService.GetExpensesAsync(objRequestInfo);
            return Ok(result);
        }

        /// <summary>
        /// Get expense details
        /// GET /api/expenses/{expensePublicId}
        /// </summary>
        [HttpGet("{expensePublicId}")]
        public async Task<IActionResult> GetExpense(Guid expensePublicId)
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();

            var result = await _expenseService.GetExpenseAsync(userPublicId, expensePublicId);
            return Ok(result);
        }

        /// <summary>
        /// Update an expense
        /// PUT /api/expenses/{expensePublicId}
        /// </summary>
        [HttpPut("{expensePublicId}")]
        public async Task<IActionResult> UpdateExpense(Guid expensePublicId, ExpenseUpdateRequestInfo objRequestInfo)
        {
            if (objRequestInfo == null)
            {
                throw new ArgumentNullException(nameof(objRequestInfo));
            }

            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();

            objRequestInfo.UserPublicId = userPublicId;
            objRequestInfo.ExpensePublicId = expensePublicId;
            var result = await _expenseService.UpdateExpenseAsync(objRequestInfo);
            return Ok(result);
        }

        /// <summary>
        /// Delete an expense (soft delete)
        /// DELETE /api/expenses/{expensePublicId}
        /// </summary>
        [HttpDelete("{expensePublicId}")]
        public async Task<IActionResult> DeleteExpense(Guid expensePublicId)
        {
            var userPublicIdClaim = HttpContext.Items["UserPublicId"]?.ToString();
            if (!Guid.TryParse(userPublicIdClaim, out var userPublicId))
                return Unauthorized();

            var result = await _expenseService.DeleteExpenseAsync(userPublicId, expensePublicId);
            return Ok(result);
        }
    }
}
