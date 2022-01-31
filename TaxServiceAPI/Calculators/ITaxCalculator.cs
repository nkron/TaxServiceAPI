using System.Threading.Tasks;
using TaxServiceAPI.Models;

namespace TaxServiceAPI.Calculators
{
    public interface ITaxCalculator
    {
        //Assuming for simplicity that all calculators could use the same models
        public Task<decimal> GetLocationTaxAsync(GetLocationTaxModel locationModel);

        public Task<decimal> GetOrderTaxAsync(GetOrderTaxModel parameters);
    }
}
