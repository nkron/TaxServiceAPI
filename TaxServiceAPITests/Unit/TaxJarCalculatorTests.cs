using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TaxServiceAPI.Calculators;
using TaxServiceAPI.Models;
using Xunit;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace TaxServiceAPITests.Unit
{
    public class TaxJarCalculatorTests
    {

        [Fact]
        public void GetLocationTaxAsyncReturnsCorrectResults()
        {
            //Arrange
            const decimal expected = (decimal)2.5;
            var mockModel = new RateModel { rate = new Rate { zip = "19123", combined_rate = expected.ToString() } };
            var mockResponse = JsonSerializer.Serialize(mockModel);
            var calculator = new TaxJarCalculator(GetMockHttpClient(mockResponse));
            var parameters = new GetLocationTaxModel(null, "19123", null, null, null);

            //Act
            var actual = calculator.GetLocationTaxAsync(parameters).Result;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetOrderTaxAsyncReturnsCorrectResults()
        {
            //Arrange
            const decimal expected = (decimal)7.7;
            var mockModel = new TaxModel { tax = new Tax { amount_to_collect = expected } };
            var mockResponse = JsonSerializer.Serialize(mockModel);
            var calculator = new TaxJarCalculator(GetMockHttpClient(mockResponse));
            var parameters = new GetOrderTaxModel(null, null, null, null, null, null, null, null, null, null, 5, 6);

            //Act
            var actual = calculator.GetOrderTaxAsync(parameters).Result;

            //Assert
            Assert.Equal(expected, actual);
        }

        private HttpClient GetMockHttpClient(string mockContent)
        {
            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(mockContent)
                });

            var client = new HttpClient(mockMessageHandler.Object);
            client.BaseAddress = new Uri("https://fakeurl.com/");

            return client;
        }

    }
}
