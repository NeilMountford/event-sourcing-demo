namespace EventSourcedVouchers.Api.EventSourcing
{
    public interface IVoucherEvent
    {
        void Apply(VoucherBuilder voucherBuilder);
    }
}