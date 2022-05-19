using apiNux.Domain;
using apiNux.Services;
using Microsoft.AspNetCore.Mvc;
using SeguroViagemApi.Application.Utils;
using System.Threading.Tasks;

namespace apiNux.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MaterialController
    {
        private readonly MaterialService _materialService;
        public MaterialController(MaterialService materialService)
        {
            _materialService = materialService;
        }

        [HttpGet]
        [Route("List-Materials")]
        public async Task<IActionResult> List()
        {
            return new ResponseHelper().CreateResponse(await _materialService.List());
        }

        [HttpPost]
        [Route("Add-Material")]
        public async Task<IActionResult> AddMaterial(Material material)
        {
            return new ResponseHelper().CreateResponse(await _materialService.AddMaterial(material));
        }
    }
}
