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
        [Route("Get-Product/{productId}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            return new ResponseHelper().CreateResponse(await _productService.GetProductById(productId));
        }

        [HttpPost]
        [Route("Get-Document/{uploadDocumentId}")]
        public IActionResult GetDocument(int uploadDocumentId)
        {
            return new ResponseHelper().CreateResponse(_productService.GetDocument(uploadDocumentId));
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
        [HttpPut]
        [Route("Add-Document")]
        public async Task<IActionResult> AddDocument(Product product)
        {
            return new ResponseHelper().CreateResponse(await _productService.AddDocument(product));
        }

        [HttpPut]
        [Route("Edit-Product")]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            return new ResponseHelper().CreateResponse(await _productService.UpdateProduct(product));
        }

        [HttpPut]
        [Route("Change-Status/{productId}")]
        public async Task<IActionResult> ChangeStatusProduct(int productId)
        {
            return new ResponseHelper().CreateResponse(await _productService.ChangeStatusProduct(productId));
        }
    }
}
