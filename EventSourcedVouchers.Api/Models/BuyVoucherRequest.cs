namespace EventSourcedVouchers.Api.Models
{
    public class BuyVoucherRequest
    {
        public BuyVoucherRequest(decimal amount)
        {
            this.Amount = amount;
        }
        
        public decimal Amount { get; }
    }
}