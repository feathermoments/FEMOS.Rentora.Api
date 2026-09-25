using FEMOS.Rentora.Domain.Requests;
using FEMOS.Rentora.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Application.Interfaces
{
    public interface IExpenseService
    {
        Task<ExpenseResponseInfo> AddExpenseAsync(ExpenseCreateRequestInfo objRequestInfo);
        Task<ExpenseResponseInfo> GetExpenseAsync(Guid userPublicId, Guid expensePublicId);
        Task<ExpenseListResponseInfo> GetExpensesAsync(FilterRequestInfo objRequestInfo);
        Task<BaseResponseInfo> UpdateExpenseAsync(ExpenseUpdateRequestInfo objRequestInfo);
        Task<BaseResponseInfo> DeleteExpenseAsync(Guid userPublicId, Guid expensePublicId);
    }
}
