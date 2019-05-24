using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcedVouchers.Api.Controllers
{
    using EventSourcedVouchers.Api.Models;

    [Route("api/[controller]")]
    [ApiController]
    public class VouchersController : ControllerBase
    {
        // POST api/values
        [HttpPost]
        [Route("buy")]
        public IActionResult Buy([FromBody] BuyVoucherRequest buyVoucherRequest)
        {
            return this.Ok(new BuyVoucherResponse("ABCD"));
        }
    }
}