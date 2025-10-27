using TransactionApi.Models;

namespace TransactionApi.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetSampleTransactionsAsync();
        Task<Dictionary<string, Dictionary<string, decimal>>> CalculateDailyTotalsAsync(IEnumerable<Transaction> transactions);
    }
}