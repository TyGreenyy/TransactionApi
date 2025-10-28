using TransactionApi.Models;
using TransactionApi.Data;
using Microsoft.EntityFrameworkCore;

namespace TransactionApi.Services
{
    public class TransactionService : ITransactionService
    {

        private readonly TransactionContext _context;

        public TransactionService(TransactionContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transaction>> GetSampleTransactionsAsync()
        {
            var results = await _context.Transactions.ToListAsync();
            return results;
        }

        public async Task<Dictionary<string, Dictionary<string,decimal>>> CalculateDailyTotalsAsync(IEnumerable<Transaction> transactions)
        {
            var result = CalculateDailyTotals(transactions);
            return await Task.FromResult(result); 
        }

        private Dictionary<string, Dictionary<string, decimal>> CalculateDailyTotals(IEnumerable<Transaction> transactions)
        {
            if (transactions == null || !transactions.Any())
            {
                return new Dictionary<string, Dictionary<string, decimal>>();
            }

            var dailyTotals = transactions
                .GroupBy(t => t.Currency)
                .ToDictionary(
                    currencyGroup => currencyGroup.Key,
                    currencyGroup => currencyGroup
                        .GroupBy(t => t.Timestamp.ToString("yyyy-MM-dd"))
                        .ToDictionary(
                            dateGroup => dateGroup.Key,
                            dateGroup => dateGroup.Sum(t => t.Amount)
                        )
                );

            return dailyTotals;
        }
    }
}