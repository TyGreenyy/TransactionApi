using Microsoft.AspNetCore.Mvc;
using TransactionApi.Services;

namespace TransactionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly TransactionService _transactionService;

        public TransactionsController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("daily-totals")]
        public IActionResult GetDailyTotals()
        {
            try
            {
                var transactions = _transactionService.GetSampleTransactions();
                var totals = _transactionService.CalculateDailyTotals(transactions);
                return Ok(totals);
            }
            catch (Exception ex)
            {
                // Log ex to logging
                return StatusCode(500, "Any internal server error occured");
            }
        }
    }
}