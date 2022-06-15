using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TariffComparison.LogicLayer.Model;
using Xunit;

namespace TariffComparison.Test
{
    public class ProductControllerTest: IClassFixture<WebApplicationFactory<TariffComparison.Startup>>
    {
        readonly HttpClient httpClient;

        public ProductControllerTest(WebApplicationFactory<TariffComparison.Startup> application)
        {
            httpClient = application.CreateClient();
        }

        [Fact]
        public async Task GET_retrieve_products_emptyConsumption()
        {
            var response = await httpClient.GetAsync("/api/product");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GET_retrive_products_notNumber()
        {
            var response = await httpClient.GetAsync("/api/product/notNumber");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData(1000, 280, 800, "basic electricity tariff")]
        [InlineData(3500, 800, 830, "Packaged tariff")]
        [InlineData(4000, 800, 940, "Packaged tariff")]
        [InlineData(4500, 950, 1050, "Packaged tariff")]
        [InlineData(6000, 1380, 1400, "basic electricity tariff")]
        [InlineData(6500, 1490, 1550, "basic electricity tariff")]
        public async Task GET_retrive_products_calculation(double consumption, double expectedProduct1, double expectedProduct2, string productname)
        {
            var response = await httpClient.GetAsync("/api/product/" + consumption.ToString());
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var products = JsonConvert.DeserializeObject<Product[]>(
                    await response.Content.ReadAsStringAsync()
                );

            products.Should().HaveCount(2);

            Assert.Equal(expectedProduct1, products[0].AnnualCost);
            Assert.Equal(productname, products[0].TariffName, true);
            Assert.Equal(expectedProduct2, products[1].AnnualCost);
        }

    }
}
