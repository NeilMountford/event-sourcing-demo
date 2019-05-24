using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcedVouchers.Api.Controllers
{
    using EventSourcedVouchers.Api.EventSourcing;
    using EventSourcedVouchers.Api.EventSourcing.Events;
    using EventSourcedVouchers.Api.Exceptions;
    using EventSourcedVouchers.Api.Models;

    [Route("api/[controller]")]
    [ApiController]
    public class VouchersController : ControllerBase
    {
        private readonly VoucherEventStore voucherEventStore;

        public VouchersController(VoucherEventStore voucherEventStore)
        {
            this.voucherEventStore = voucherEventStore;
        }
        
        // POST api/values
        [HttpPost]
        [Route("buy")]
        public IActionResult Buy([FromBody] BuyVoucherRequest buyVoucherRequest)
        {
            var newVoucherCode = Guid.NewGuid().ToString();
            this.voucherEventStore.AppendEvent(newVoucherCode, new VoucherPurchased(buyVoucherRequest.Amount));
            return this.Ok(new BuyVoucherResponse(newVoucherCode));
        }

        [HttpGet]
        [Route("{voucherCode}")]
        public IActionResult Get(string voucherCode)
        {
            try
            {
                var voucher = this.voucherEventStore.ProjectReadModel(voucherCode);
                return this.Ok(voucher);
            }
            catch (VoucherNotFoundException)
            {
                return this.NotFound();
            }
        }
    }
}