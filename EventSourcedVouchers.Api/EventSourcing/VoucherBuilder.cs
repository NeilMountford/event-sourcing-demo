namespace EventSourcedVouchers.Api.EventSourcing
{
    using EventSourcedVouchers.Api.Models;

    public class VoucherBuilder
    {
        private string voucherCode;
        private decimal originalAmount;
        private decimal currentAmount;

        public VoucherBuilder(string voucherCode)
        {
            this.voucherCode = voucherCode;
            this.originalAmount = 0;
            this.currentAmount = 0;
        }

        public void SetStartingAmount(decimal startingAmount)
        {
            this.originalAmount = startingAmount;
            this.currentAmount = startingAmount;
        }

        public Voucher Build()
        {
            return new Voucher(this.voucherCode, this.originalAmount, this.currentAmount);
        }
    }
}