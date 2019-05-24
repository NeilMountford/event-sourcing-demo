namespace EventSourcedVouchers.Api.EventSourcing.Events
{
    public class VoucherPurchased : IVoucherEvent
    {
        private readonly decimal amount;

        public VoucherPurchased(decimal amount)
        {
            this.amount = amount;
        }
        
        public void Apply(VoucherBuilder voucherBuilder)
        {
            voucherBuilder.SetStartingAmount(this.amount);
        }
    }
}