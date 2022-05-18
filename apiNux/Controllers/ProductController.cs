using apiNux.Data;
using apiNux.Domain;
using apiNux.Models;
using apiNux.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SeguroViagemApi.Application.Utils;
using System.Threading.Tasks;

namespace apiNux.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }
        [HttpPost]
        [Route("List-Producs")]
        public async Task<IActionResult> List()
        {
            try
            {
                return new ResponseHelper().CreateResponse(await _productService.ListProducts());
            }
            catch
            {
                return StatusCode(500, "Ocorreu um erro");
            }
        }
        [HttpPost]
        [Route("List-ProductsWithFilter")]
        public IActionResult ListWithFilter(ProductFilterModel filter)
        {
            try
            {
                return Ok(_productService.ListWithFilter(filter));
            }
            catch
            {
                return StatusCode(500, "Ocorreu um erro");
            }
        }

        [HttpPost]
        [Route("Create-Product")]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            return new ResponseHelper().CreateResponse(await _productService.CreateProduct(product));
        }
    }
}
