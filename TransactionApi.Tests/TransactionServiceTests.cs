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
                new Transaction(100.50m, "USD", new DateTime(2023, 10, 26, 8, 30, 0)),
                new Transaction(75.00m, "USD", new DateTime(2023, 10, 26, 14, 45, 0)),
                new Transaction(200.00m, "EUR", new DateTime(2023, 10, 26, 9, 0, 0)),
                new Transaction(50.25m, "USD", new DateTime(2023, 10, 27, 10, 15, 0)),
                new Transaction(150.75m, "EUR", new DateTime(2023, 10, 27, 11, 0, 0)),
                new Transaction(99.99m, "EUR", new DateTime(2023, 10, 27, 16, 30, 0)),
                new Transaction(300.00m, "JPY", new DateTime(2023, 10, 27, 12, 0, 0)),
            };

            // Expected Outcomes
            var expectedUsdTotalForDay1 = 100.50m + 75.00m;
            var expectedUsdTotalForDay2 = 50.25m;
            
            var expectedEurTotalForDay1 = 200.00m;
            var expectedEurTotalForDay2 = 150.75m + 99.99m;

            var expectedJpyTotalForDay1 = 300.00m;

            var day1 = "2023-10-26"; 
            var day2 = "2023-10-27"; 

            var result = _service.CalculateDailyTotals(transactions);

            Assert.Equal(3, result.Count);

            Assert.True(result.ContainsKey("USD")); 
            Assert.True(result.ContainsKey("EUR"));
            Assert.True(result.ContainsKey("JPY"));

            // Check USD
            var usdTotals = result["USD"]; 
            Assert.Equal(2, usdTotals.Count); 
            Assert.Equal(expectedUsdTotalForDay1, usdTotals[day1]);
            Assert.Equal(expectedUsdTotalForDay2, usdTotals[day2]); 

            var eurTotals = result["EUR"];
            Assert.Equal(2, eurTotals.Count); 
            Assert.Equal(expectedEurTotalForDay1, eurTotals[day1]);
            Assert.Equal(expectedEurTotalForDay2, eurTotals[day2]);

            var jpyTotals = result["JPY"];
            Assert.Single(jpyTotals);
            Assert.Equal(expectedJpyTotalForDay1, jpyTotals[day2]);
        }
    }
}