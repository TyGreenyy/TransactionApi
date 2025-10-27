using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using TransactionApi.Services;

namespace TransactionApi.Controllers
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/transactions")]
    public class TransactionsControllerErrorTesting : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        private readonly ILogger<TransactionsController> _logger;
        public TransactionsControllerErrorTesting(ITransactionService transactionService, ILogger<TransactionsController> logger)
        {
            _transactionService = transactionService;
            _logger = logger;
        }

        [HttpGet("daily-totals")]
        public Task<IActionResult> GetDailyTotals()
        {
            try
            {
                throw new InvalidOperationException("This is a test exception!");
                // var transactions = await _transactionService.GetSampleTransactionsAsync();
                // var totals = await _transactionService.CalculateDailyTotalsAsync(transactions);

                // return Ok(totals);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occured while calculating daily totals");

                return Task.FromResult<IActionResult>(StatusCode(500, "Any internal server error occured"));
            }
        }
    }
}