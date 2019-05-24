namespace EventSourcedVouchers.Api.Models
{
    public class Voucher
    {
        public Voucher(string voucherCode, decimal originalAmount, decimal currentAmount)
        {
            this.VoucherCode = voucherCode;
            this.OriginalAmount = originalAmount;
            this.CurrentAmount = currentAmount;
        }

        public string VoucherCode { get; }
        public decimal OriginalAmount { get; }
        public decimal CurrentAmount { get; }
    }
}