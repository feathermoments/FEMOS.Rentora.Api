using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Responses
{
    public class ExpenseSummaryResponseInfo : BaseResponseInfo
    {
        public decimal TotalExpense { get; set; }
        public int ExpenseCount { get; set; }
        public List<ExpenseCategorySummaryInfo> Categories { get; set; } = new List<ExpenseCategorySummaryInfo>();
        public List<ExpenseMonthlyTrendInfo> MonthlyTrend { get; set; } = new List<ExpenseMonthlyTrendInfo>();
    }

    public class ExpenseCategorySummaryInfo
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public decimal Amount { get; set; }
    }

    public class ExpenseMonthlyTrendInfo
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal Amount { get; set; }
    }
}
