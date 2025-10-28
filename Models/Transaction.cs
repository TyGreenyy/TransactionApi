using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// { 
//     public record Transaction(decimal Amount, string Currency, DateTime Timestamp); 
// }

namespace TransactionApi.Models
{
    [Table("Transactions")]
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }
}