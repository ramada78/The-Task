using ElkoodTask.Servies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElkoodTask.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductTypesController : ControllerBase
    {
        private readonly IProductTypesService productTypesService;

        public ProductTypesController(IProductTypesService productTypesService)
        {
            this.productTypesService = productTypesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var ProductType = await productTypesService.GetAll();
            return Ok(ProductType);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(ProductTypeDto dto)
        {
            var productType = new ProductType { Name = dto.Name };
            await productTypesService.Add(productType);
            return Ok(productType);
        }
 
    }
}
