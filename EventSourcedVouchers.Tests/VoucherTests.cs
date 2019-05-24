namespace EventSourcedVouchers.Tests
{
    using System;
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

    public class VoucherTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient client;

        public VoucherTests(WebApplicationFactory<Startup> factory)
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
        
        [Fact]
        public async Task WhenAVoucherIsBoughtTheVoucherInformationCanBeRetrieved()
        {
            var buyRequest = new BuyVoucherRequest(50m);
            string voucherCode;
            
            using (var requestContent =
                new StringContent(JsonConvert.SerializeObject(buyRequest), Encoding.UTF8, "application/json"))
            {
                var buyResponse = await this.client.PostAsync("/api/vouchers/buy", requestContent);
                var buyVoucherResponse = JsonConvert.DeserializeObject<BuyVoucherResponse>(await buyResponse.Content.ReadAsStringAsync());
                voucherCode = buyVoucherResponse.VoucherCode;
            }
            
            var getResponse = await this.client.GetAsync($"/api/vouchers/{voucherCode}");
            getResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
            var voucher = JsonConvert.DeserializeObject<Voucher>(await getResponse.Content.ReadAsStringAsync());
            voucher.VoucherCode.ShouldBe(voucherCode);
            voucher.OriginalAmount.ShouldBe(50m);
            voucher.CurrentAmount.ShouldBe(50m);
        }
        
        [Fact]
        public async Task WhenAVoucherThatDoesNotExistIsRequestedANotFoundIsGiven()
        {
            var fakeCode = Guid.NewGuid().ToString();
            var getResponse = await this.client.GetAsync($"/api/vouchers/{fakeCode}");
            getResponse.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
        
        [Fact]
        public async Task WhenAVoucherIsPartiallySpentTheCurrentAmountIsCorrect()
        {
            var buyRequest = new BuyVoucherRequest(50m);
            string voucherCode;
            
            using (var requestContent =
                new StringContent(JsonConvert.SerializeObject(buyRequest), Encoding.UTF8, "application/json"))
            {
                var buyResponse = await this.client.PostAsync("/api/vouchers/buy", requestContent);
                var buyVoucherResponse = JsonConvert.DeserializeObject<BuyVoucherResponse>(await buyResponse.Content.ReadAsStringAsync());
                voucherCode = buyVoucherResponse.VoucherCode;
            }
            
            var spendRequest = new SpendVoucherRequest(voucherCode, 25);
            
            using (var requestContent =
                new StringContent(JsonConvert.SerializeObject(spendRequest), Encoding.UTF8, "application/json"))
            {
                var spendResponse = await this.client.PostAsync("/api/vouchers/spend", requestContent);
                spendResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
            }
            
            var getResponse = await this.client.GetAsync($"/api/vouchers/{voucherCode}");
            getResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
            var voucher = JsonConvert.DeserializeObject<Voucher>(await getResponse.Content.ReadAsStringAsync());
            voucher.VoucherCode.ShouldBe(voucherCode);
            voucher.OriginalAmount.ShouldBe(50m);
            voucher.CurrentAmount.ShouldBe(25m);
        }
    }
}