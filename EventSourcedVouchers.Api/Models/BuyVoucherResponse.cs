namespace EventSourcedVouchers.Api.Models
{
    public class BuyVoucherResponse
    {
        public BuyVoucherResponse(string voucherCode)
        {
            this.VoucherCode = voucherCode;
        }
        
        public string VoucherCode { get; }
    }
}