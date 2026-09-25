using FEMOS.Rentora.Domain.Entities;
using FEMOS.Rentora.Domain.Requests;
using FEMOS.Rentora.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Infrastructure.Interfaces
{
    public interface IExpenseRepository
    {
        Task<ExpenseResponseInfo> AddExpenseAsync(ExpenseCreateRequestInfo objRequestInfo);
        Task<ExpenseResponseInfo> GetExpenseAsync(Guid userPublicId, Guid expensePublicId);
        Task<ExpenseListResponseInfo> GetExpensesAsync(FilterRequestInfo objRequestInfo);
        Task<BaseResponseInfo> UpdateExpenseAsync(ExpenseUpdateRequestInfo objRequestInfo);
        Task<BaseResponseInfo> DeleteExpenseAsync(Guid userPublicId, Guid expensePublicId);
        Task<ExpenseSummaryResponseInfo> GetExpenseSummaryAsync(Guid userPublicId, Guid? propertyPublicId, Guid? unitPublicId, DateTime? fromDate, DateTime? toDate);
    }
}
