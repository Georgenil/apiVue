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
        [HttpGet]
        [Route("List-Products")]
        public async Task<IActionResult> ListProducts()
        {
            return new ResponseHelper().CreateResponse(await _productService.ListProducts());
        }
        [HttpPost]
        [Route("List-ProductsWithFilter")]
        public IActionResult ListWithFilter(ProductFilterModel filter)
        {
            return Ok(_productService.ListWithFilter(filter));
        }

        [HttpPost]
        [Route("Add-Product")]
        public async Task<IActionResult> AddProduct(Product product)
        {
            return new ResponseHelper().CreateResponse(await _productService.AddProduct(product));
        }
    }
}
