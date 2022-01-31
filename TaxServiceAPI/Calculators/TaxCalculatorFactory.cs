using System;
using System.Net.Http;

namespace TaxServiceAPI.Calculators
{
    public interface ITaxCalculatorFactory
    {
        ITaxCalculator GetTaxJarCalculator();
        ITaxCalculator GetAnotherTaxCalculator();
    }

    public class TaxCalculatorFactory : ITaxCalculatorFactory
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TaxCalculatorFactory(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public ITaxCalculator GetTaxJarCalculator()
        {
            var httpClient = _httpClientFactory.CreateClient("TaxJar");
            return new TaxJarCalculator(httpClient);
        }

        public ITaxCalculator GetAnotherTaxCalculator()
        {
            throw new NotImplementedException("Example future calculator");
        }
    }
}
