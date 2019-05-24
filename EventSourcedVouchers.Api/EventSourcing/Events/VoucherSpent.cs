namespace EventSourcedVouchers.Api.EventSourcing.Events
{
    public class VoucherSpent : IVoucherEvent
    {
        private readonly decimal amountSpent;

        public VoucherSpent(decimal amountSpent)
        {
            this.amountSpent = amountSpent;
        }
        
        public void Apply(VoucherBuilder voucherBuilder)
        {
            voucherBuilder.AdjustCurrentAmount(-this.amountSpent);
        }
    }
}