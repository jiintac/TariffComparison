using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TariffComparison.LogicLayer.Model;
using TariffComparison.LogicLayer.Service;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TariffComparison.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductService productService;
        public ProductController(IProductService product)
        {
            productService = product;
        }

        [HttpGet("{annualConsumption}")]
        public async Task<IActionResult> Get(string annualConsumption)
        {
            double consumption = 0;
            if (annualConsumption.Trim().Length <= 0 || !Double.TryParse(annualConsumption, out consumption))
            {
                return BadRequest("Annual Consumption is empty or not valid");
            }
            var services = await productService.GetListConsumptionAsync(consumption);
            if (services.errorcode == 0)
            {
                return new JsonResult(services.data);
            } else
            {
                return StatusCode(services.errorcode, services.errormessage);
            }
            
        }
    }
}
