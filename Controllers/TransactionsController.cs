using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TransactionApi.Services;

namespace TransactionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        private readonly ILogger<TransactionsController> _logger;
        public TransactionsController(TransactionService transactionService, ILogger<TransactionsController> logger)
        {
            _transactionService = transactionService;
            _logger = logger;
        }

        [HttpGet("daily-totals")]
        public async Task<IActionResult> GetDailyTotals()
        {
            try
            {
                // throw new InvalidOperationException("This is a test exception!");
                var transactions = await _transactionService.GetSampleTransactionsAsync();
                var totals = await _transactionService.CalculateDailyTotalsAsync(transactions);

                return Ok(totals);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occured while calculating daily totals");

                return StatusCode(500, "Any internal server error occured");
            }
        }
    }
}