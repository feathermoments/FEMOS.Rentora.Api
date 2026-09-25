using FEMOS.Rentora.Application.Interfaces;
using FEMOS.Rentora.Domain.Requests;
using FEMOS.Rentora.Domain.Responses;
using FEMOS.Rentora.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Application.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseService(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<ExpenseResponseInfo> AddExpenseAsync(ExpenseCreateRequestInfo objRequestInfo)
        {
            return await _expenseRepository.AddExpenseAsync(objRequestInfo);
        }

        public async Task<ExpenseResponseInfo> GetExpenseAsync(Guid userPublicId, Guid expensePublicId)
        {
            return await _expenseRepository.GetExpenseAsync(userPublicId, expensePublicId);
        }

        public async Task<ExpenseListResponseInfo> GetExpensesAsync(FilterRequestInfo objRequestInfo)
        {
            return await _expenseRepository.GetExpensesAsync(objRequestInfo);
        }

        public async Task<BaseResponseInfo> UpdateExpenseAsync(ExpenseUpdateRequestInfo objRequestInfo)
        {
            return await _expenseRepository.UpdateExpenseAsync(objRequestInfo);
        }

        public async Task<BaseResponseInfo> DeleteExpenseAsync(Guid userPublicId, Guid expensePublicId)
        {
            return await _expenseRepository.DeleteExpenseAsync(userPublicId, expensePublicId);
        }
    }
}
