using Moq;
using System.Threading.Tasks;
using TaxServiceAPI;
using TaxServiceAPI.Calculators;
using TaxServiceAPI.Models;
using Xunit;

namespace TaxServiceAPITests.Unit
{
    public class TaxServiceTests
    {
        private const decimal ExpectedDecimal = (decimal)4.9;

        [Fact]
        public void GetLocationTaxAsyncReturnsCorrectResults()
        {
            //Arrange
            decimal expected = ExpectedDecimal;
            var taxService = new TaxService(GetMockCalculatorFactory());

            //Act
            var actual = taxService.GetLocationTaxAsync(new GetLocationTaxModel()).Result;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetOrderTaxAsyncReturnsCorrectResults()
        {
            //Arrange
            decimal expected = ExpectedDecimal;
            var taxService = new TaxService(GetMockCalculatorFactory());

            //Act
            var actual = taxService.GetOrderTaxAsync(new GetOrderTaxModel()).Result;

            //Assert
            Assert.Equal(expected, actual);
        }

        private ITaxCalculatorFactory GetMockCalculatorFactory()
        {
            var mockFactory = new Mock<ITaxCalculatorFactory>();
            mockFactory
                .Setup(x => x.GetTaxJarCalculator())
                .Returns(GetMockTaxJarCalculator());

            return mockFactory.Object;
        }

        private ITaxCalculator GetMockTaxJarCalculator()
        {
            var mockCalculator = new Mock<ITaxCalculator>();

            mockCalculator
                .Setup<Task<decimal>>(x => x.GetLocationTaxAsync(It.IsAny<GetLocationTaxModel>()))
                .ReturnsAsync(ExpectedDecimal);

            mockCalculator
                .Setup<Task<decimal>>(x => x.GetOrderTaxAsync(It.IsAny<GetOrderTaxModel>()))
                .ReturnsAsync(ExpectedDecimal);

            return mockCalculator.Object;
        }
    }
}
