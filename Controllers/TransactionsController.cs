using Microsoft.AspNetCore.Mvc;
using TransactionApi.Services;

namespace TransactionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly TransactionService _transactionService;

        private readonly ILogger<TransactionsController> _logger;
        public TransactionsController(TransactionService transactionService, ILogger<TransactionsController> logger)
        {
            _transactionService = transactionService;
            _logger = logger;
        }

        [HttpGet("daily-totals")]
        public IActionResult GetDailyTotals()
        {
            try
            {
                // throw new InvalidOperationException("This is a test exception!");
                var transactions = _transactionService.GetSampleTransactions();
                var totals = _transactionService.CalculateDailyTotals(transactions);
                
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