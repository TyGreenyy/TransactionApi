using TransactionApi.Models;

namespace TransactionApi.Services
{
    public class TransactionService
    {
        public IEnumerable<Transaction> GetSampleTransactions()
        {
            return new List<Transaction>
            {
                new Transaction(100.50m, "USD", new DateTime(2023, 10, 26, 8, 30, 0)),
                new Transaction(75.00m, "USD", new DateTime(2023, 10, 26, 14, 45, 0)),
                new Transaction(200.00m, "EUR", new DateTime(2023, 10, 26, 9, 0, 0)),
                new Transaction(50.25m, "USD", new DateTime(2023, 10, 27, 10, 15, 0)),
                new Transaction(150.75m, "EUR", new DateTime(2023, 10, 27, 11, 0, 0)),
                new Transaction(99.99m, "EUR", new DateTime(2023, 10, 27, 16, 30, 0)),
                new Transaction(300.00m, "JPY", new DateTime(2023, 10, 27, 12, 0, 0)),
            };
        }

        public Dictionary<string, Dictionary<string, decimal>> CalculateDailyTotals(IEnumerable<Transaction> transactions)
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