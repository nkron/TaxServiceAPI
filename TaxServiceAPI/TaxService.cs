using System.Threading.Tasks;
using TaxServiceAPI.Calculators;
using TaxServiceAPI.Models;

namespace TaxServiceAPI
{
    public interface ITaxService
    {
        Task<decimal> GetLocationTaxAsync(GetLocationTaxModel location, string customer = "TaxJar");
        Task<decimal> GetOrderTaxAsync(GetOrderTaxModel getOrderTaxModel, string customer = "TaxJar");
    }

    public class TaxService : ITaxService
    {
        private readonly ITaxCalculatorFactory _calculatorFactory;

        public TaxService(ITaxCalculatorFactory calculatorFactory)
        {
            _calculatorFactory = calculatorFactory;
        }

        public async Task<decimal> GetLocationTaxAsync(GetLocationTaxModel location, string customer = "TaxJar")
        {
            var calculator = GetTaxCalculatorForCustomer(customer);

            var taxToCollect = await calculator.GetLocationTaxAsync(location);

            return taxToCollect;
        }

        public async Task<decimal> GetOrderTaxAsync(GetOrderTaxModel getOrderTaxModel, string customer = "TaxJar")
        {
            var calculator = GetTaxCalculatorForCustomer(customer);

            var taxToCollect = await calculator.GetOrderTaxAsync(getOrderTaxModel);

            return taxToCollect;
        }

        //Simple example of how we could get different calculators for different customers
        private ITaxCalculator GetTaxCalculatorForCustomer(string customer)
        {
            switch (customer)
            {
                case "TaxJar":
                    return _calculatorFactory.GetTaxJarCalculator();
                case "AnotherTaxCalculator":
                    return _calculatorFactory.GetAnotherTaxCalculator();
                default:
                    //Throw custom error/log
                    return null;
            }

        }
    }
}
