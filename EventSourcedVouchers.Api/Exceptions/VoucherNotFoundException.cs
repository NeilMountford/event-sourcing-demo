namespace EventSourcedVouchers.Api.Exceptions
{
    using System;

    public class VoucherNotFoundException : Exception
    {
        public VoucherNotFoundException(string voucherCode) 
            : base($"Voucher with code {voucherCode} was not found")
        {
        }
    }
}