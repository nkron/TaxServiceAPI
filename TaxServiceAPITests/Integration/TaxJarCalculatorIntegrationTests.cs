using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using TaxServiceAPI.Calculators;
using TaxServiceAPI.Models;
using Xunit;

namespace TaxServiceAPITests.Integration
{
    public class TaxJarCalculatorIntegrationTests : IClassFixture<TaxJarCalculatorIntegrationTests.DefaultHttpClientFactory>
    {
        private readonly IHttpClientFactory _clientFactory;

        public TaxJarCalculatorIntegrationTests(DefaultHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [Fact]
        public void TaxJarCalculatorGetsResultsFromApi()
        {
            //Arrange
            var calculator = new TaxJarCalculator(GetTaxJarClient());
            var parameters = new GetLocationTaxModel(null, "19123", null, null, null);

            //Act
            var result = calculator.GetLocationTaxAsync(parameters).Result;

            //Assert
            Assert.NotEqual(0, result);
        }

        [Fact]
        public void GetOrderTaxesGetsResultsFromApi()
        {
            //Arrange
            var calculator = new TaxJarCalculator(GetTaxJarClient());
            var model = GetMockOrderTaxModel();

            //Act
            var result = calculator.GetOrderTaxAsync(model).Result;

            //Assert
            Assert.NotEqual(0, result);
        }

        private GetOrderTaxModel GetMockOrderTaxModel()
        {
            var mockJson = "{\"from_country\":\"US\",\"from_zip\":\"92093\",\"from_state\":\"CA\",\"from_city\":\"LaJolla\",\"from_street\":\"9500GilmanDrive\",\"to_country\":\"US\",\"to_zip\":\"90002\",\"to_state\":\"CA\",\"to_city\":\"LosAngeles\",\"to_street\":\"1335E103rdSt\",\"amount\":15,\"shipping\":1.5}";

            return JsonSerializer.Deserialize<GetOrderTaxModel>(mockJson);
        }

        private HttpClient GetTaxJarClient()
        {
            var client = _clientFactory.CreateClient();
            client.BaseAddress = new Uri("https://api.taxjar.com/v2/");
            client.DefaultRequestHeaders.Authorization =
                //Replace with test appsettings var
                new AuthenticationHeaderValue("Bearer", "itsasecret");

            return client;
        }

        public class DefaultHttpClientFactory : IHttpClientFactory
        {
            public HttpClient CreateClient(string name) => new HttpClient();
        }
    }
}
