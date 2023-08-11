using ElkoodTask.Dtos;
using ElkoodTask.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElkoodTask.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var products = await _context.Product
                .Include(pi => pi.ProductType)
                .Select(pi => new ProductDetailsDto
                {
                    Id = pi.Id,
                    Name = pi.Name,
                    ProductTypeName = pi.ProductType.Name
                })
                .ToListAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ProductDto dto)
        {
            var isValidProductType = await _context.ProductType.AnyAsync(pi => pi.Id == dto.ProductTypeId);
            if (!isValidProductType)
            {
                return BadRequest(error: "Invalid Product Type");
            }
          
            var product = new Product
            {
                Name = dto.Name,
                ProductTypeId = dto.ProductTypeId
            };
            await _context.AddAsync(product);
            _context.SaveChanges();
            return Ok(product);
        }
       
    }
}
