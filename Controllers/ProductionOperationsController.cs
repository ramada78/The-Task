using ElkoodTask.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElkoodTask.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductionOperationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductionOperationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var productionOperations = await _context.Production_Operation
                .Include(po => po.Product)
                .Select(po => new ProductionDetailsDto
                {
                    Id = po.Id,
                    BranchName = po.Branch.Name,
                    ProductName = po.Product.Name, 
                    quantity = po.Quantity,
                    RemainingQuantity = po.RemainingQuantity,  
                    date = po.Date
                })
                .ToListAsync();
             return Ok(productionOperations);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ProductionOprerationDto dto)
        {
            var isValidBranch = await _context.Branch.AnyAsync(bi => bi.Id == dto.BranchId);
            var isValidProduct = await _context.Product.AnyAsync(pi => pi.Id == dto.ProductId);
            var isValidBranchType = await _context.Branch
                .Where(bi => bi.BranchTypeId == 1)
                .AnyAsync(bi => bi.Id == dto.BranchId);
          
            if (!isValidBranch)
            {
                return BadRequest(error: "Invalid Branch ID");
            }
            if (!isValidProduct)
            {
                return BadRequest(error: "Invalid Product ID");
            }
            if (!isValidBranchType)
            {
                return BadRequest(error: "Invalid Branch Type ID... you can only USE ID:1 (Primary) for production");
            }

            var productionOperation = new Production_Operation
            {
                ProductId = dto.ProductId,
                BranchId = dto.BranchId,
                Quantity = dto.quantity,
                Date = dto.date
            };
            await _context.AddAsync(productionOperation);
            _context.SaveChanges();
            return Ok(productionOperation);
        }
    }
}
