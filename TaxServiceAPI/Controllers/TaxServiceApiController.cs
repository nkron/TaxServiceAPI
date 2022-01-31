using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace TaxServiceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaxServiceApiController : ControllerBase
    {
        private readonly ITaxService _taxService;

        public TaxServiceApiController(ITaxService taxService)
        {
            _taxService = taxService;
        }

        [HttpGet]
        public Task<decimal> GetLocationTax()
        {
            //Call tax service method
            throw new NotImplementedException();
        }

        [HttpPost]
        public Task<decimal> GetOrderTax()
        {
            //Call tax service method
            throw new NotImplementedException();
        }
    }
}
