using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TaxServiceAPI.Models;

namespace TaxServiceAPI.Calculators
{
    public class TaxJarCalculator : ITaxCalculator
    {
        private readonly HttpClient _httpClient;

        public TaxJarCalculator(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<decimal> GetLocationTaxAsync(GetLocationTaxModel locationModel)
        {
            var uri = _httpClient.BaseAddress + $"rates/{locationModel.Zip}";
            var response = await _httpClient.GetAsync(uri);
            var contentStream = await response.Content.ReadAsStreamAsync();

            var rateModel = await JsonSerializer.DeserializeAsync<RateModel>(contentStream);

            decimal taxOwed = 0;
            if (rateModel.rate.combined_rate != null)
            {
                taxOwed = decimal.Parse(rateModel.rate.combined_rate);
            }

            return taxOwed;
        }

        public async Task<decimal> GetOrderTaxAsync(GetOrderTaxModel orderModel)
        {
            var uri = _httpClient.BaseAddress + "taxes";
            var modelJson = JsonSerializer.Serialize(orderModel);
            var content = new StringContent(modelJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, content);
            var contentStream = await response.Content.ReadAsStreamAsync();

            var taxModel = await JsonSerializer.DeserializeAsync<TaxModel>(contentStream);

            return taxModel.tax.amount_to_collect;
        }
    }
}
