namespace TransactionApi.Models
{ 
    public record Transaction(decimal Amount, string Currency, DateTime Timestamp); 
}