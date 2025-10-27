using TransactionApi.Models;
using TransactionApi.Services;
using Xunit; 

namespace TransactionApi.Tests
{
    public class TransactionServiceTests
    {
        private readonly TransactionService _service;

        public TransactionServiceTests()
        {
            _service = new TransactionService();
        }

        [Fact] 
        public void CalculateDailyTotals_WithNullInput_ReturnsEmptyDictionary()
        {
            List<Transaction>? transactions = null;

            var result = _service.CalculateDailyTotals(transactions!);

            Assert.NotNull(result); // Should not be null
            Assert.Empty(result); // Shuold be empty 
        }

        [Fact]
        public void CalculateDailyTotals_WithEmptyList_ReturnsEmptyDictionary()
        {
            var transactions = new List<Transaction>(); 

            var result = _service.CalculateDailyTotals(transactions);

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact] 
        public void CalculateDailyTotals_WithMultipleTransactions_ReturnsCorrectlyNestedAndSummedTotals()
        { 
            var transactions = new List<Transaction> 
            { 
                new Transaction(100.00m, "USD", new DateTime(2023, 10, 26, 10, 0, 0)),
                new Transaction(50.50m, "USD", new DateTime(2023, 10, 26, 12, 0, 0)), // Same day, same currency
                new Transaction(200.00m, "EUR", new DateTime(2023, 10, 26, 11, 0, 0)),
                new Transaction(75.25m, "USD", new DateTime(2023, 10, 27, 9, 0, 0))
            };

            // Expected Outcomes
            var expectedUsdTotalForDay1 = 150.50m;
            var expectedUsdTotalForDay2 = 75.25m; 
            var expectedEurTotalForDay1 = 200.00m;
            var day1 = "2023-10-26"; 
            var day2 = "2023-10-27"; 

            var result = _service.CalculateDailyTotals(transactions);

            Assert.Equal(2, result.Count);

            Assert.True(result.ContainsKey("USD")); 
            Assert.True(result.ContainsKey("EUR"));

            // Check USD
            var usdTotals = result["USD"]; 
            Assert.Equal(2, usdTotals.Count); 
            Assert.Equal(expectedUsdTotalForDay1, usdTotals[day1]);
            Assert.Equal(expectedUsdTotalForDay2, usdTotals[day2]); 

            var eurTotals = result["EUR"];
            Assert.Single(eurTotals); 
            Assert.Equal(expectedEurTotalForDay1, eurTotals[day1]);
        }
    }
}