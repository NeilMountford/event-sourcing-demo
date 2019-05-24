namespace EventSourcedVouchers.Api.Models
{
    public class SpendVoucherRequest
    {
        public SpendVoucherRequest(string voucherCode, decimal amount)
        {
            this.VoucherCode = voucherCode;
            this.Amount = amount;
        }

        public string VoucherCode { get; }
        public decimal Amount { get; }
    }
}