using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElkoodTask.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AllProductsProducedController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AllProductsProducedController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductProducedDetailsDto>>> GetProductDetails(string companyName, DateTime startDate, DateTime endDate)
        {
            var isValidcompanyName = await _context.Company.AnyAsync(ci => ci.Name == companyName);
            if (!isValidcompanyName)
            {
                return BadRequest(error: "Invalid Company Name");
            }

            var productDetails = await _context.Production_Operation
                            .Include(p => p.Branch)
                            .ThenInclude(b => b.Company)
                            .Include(p => p.Product)
                            .Where(p => p.Branch.Company.Name == companyName &&
                                        p.Date >= startDate &&
                                        p.Date <= endDate)
                            .GroupBy(p => p.Product.Name)
                            .Select(g => new ProductProducedDetailsDto
                            {
                                ProductName = g.Key,
                                TotalQuantity = g.Sum(p => p.Quantity)
                            })
                            .ToListAsync();

            if (productDetails == null || !productDetails.Any())
            {
                return Content("There is no production operation found in that range");
            }

            return Ok(productDetails);
        }
    }
}
