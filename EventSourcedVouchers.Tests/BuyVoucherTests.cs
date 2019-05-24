namespace EventSourcedVouchers.Tests
{
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using EventSourcedVouchers.Api;
    using EventSourcedVouchers.Api.Models;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Newtonsoft.Json;
    using Shouldly;
    using Xunit;

    public class BuyVoucherTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient client;

        public BuyVoucherTests(WebApplicationFactory<Startup> factory)
        {
            this.client = factory.CreateClient();
        }
        
        [Fact]
        public async Task WhenAVoucherIsBoughtTheCodeIsReturned()
        {
            var buyRequest = new BuyVoucherRequest(50m);
            
            using (var requestContent =
                new StringContent(JsonConvert.SerializeObject(buyRequest), Encoding.UTF8, "application/json"))
            {
                var buyResponse = await this.client.PostAsync("/api/vouchers/buy", requestContent);
                buyResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
                
                var buyVoucherResponse = JsonConvert.DeserializeObject<BuyVoucherResponse>(await buyResponse.Content.ReadAsStringAsync());
                buyVoucherResponse.VoucherCode.ShouldNotBeNullOrEmpty();
            }
        }
    }
}