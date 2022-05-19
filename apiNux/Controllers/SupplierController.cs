using apiNux.Domain;
using apiNux.Services;
using Microsoft.AspNetCore.Mvc;
using SeguroViagemApi.Application.Utils;
using System.Threading.Tasks;

namespace apiNux.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SupplierController
    {
        private readonly SupplierService _supplierService;
        public SupplierController(SupplierService supplierService)
        {
            _supplierService = supplierService;
        }
        [HttpGet]
        [Route("List-Suppliers")]
        public async Task<IActionResult> ListSuppliers()
        {
            return new ResponseHelper().CreateResponse(await _supplierService.ListSuppliers());
        }
        [HttpPost]
        [Route("Add-Supplier")]
        public async Task<IActionResult> AddSupplier(Supplier supplier)
        {
            return new ResponseHelper().CreateResponse(await _supplierService.AddSupplier(supplier));
        }
    }
}
