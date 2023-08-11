using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElkoodTask.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsProducedController : ControllerBase
    {
        private ApplicationDbContext _context;
        public ProductsProducedController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Get(string companyName, int primaryBranchId, DateTime fromDate, DateTime toDate)
        {
            var primaryBranch = _context.Branch.FirstOrDefault(b => b.Id == primaryBranchId && b.Company.Name.Equals(companyName) && b.BranchType.Name.Equals("Primary"));
            if (primaryBranch == null)
            {
                return BadRequest("Invalid branch or company name");
            }
            var quantitiesByProductName = _context.Production_Operation
                .Where(p => p.BranchId == primaryBranchId && p.Date >= fromDate && p.Date <= toDate)
                .GroupBy(p => p.Product.Name)
                .Select(g => new { ProductName = g.Key, TotalQuantityProduced = g.Sum(p => p.Quantity) })
                .ToList();

            return Ok(quantitiesByProductName);
        }
    }
}
