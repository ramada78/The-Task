using ElkoodTask.Dtos;
using ElkoodTask.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElkoodTask.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DistributionOperationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DistributionOperationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var distributionOperations = await _context.Distribution_Operation
                .Include(dio => dio.Product)
                .Select(dio => new DistrubutionDetailsDto
                {
                    Id = dio.Id,
                    PrimaryBranchName = dio.PrimaryBranch.Name,
                    SecondaryBranchName = dio.SecondaryBranch.Name,
                    ProductName = dio.Product.Name,
                    Quantity = dio.Quantity,
                    Date = dio.Date
                })
                .ToListAsync();
            return Ok(distributionOperations);
        }
        

        [HttpPost]
        public async Task<IActionResult> AddDistributionOperation([FromBody] DistrubutionOperationDto dto)
        {
            var totalRemainingQuantity = await _context.Production_Operation
                .Where(po => po.BranchId == dto.PrimaryBranchId && po.ProductId == dto.ProductId)
                .SumAsync(po => po.RemainingQuantity);
            if (totalRemainingQuantity < dto.quantity)
            {
                return BadRequest("Not enough remaining product quantity");
            }

            var isValidPrimaryBranchType = await _context.Branch
                .Where(bt => bt.BranchTypeId == 1)
                .AnyAsync(bt => bt.Id == dto.PrimaryBranchId);
            if (!isValidPrimaryBranchType)
            {
                return BadRequest(error: "Invalid Branch Type ID..");
            }

            var isValidSecondaryBranchType = await _context.Branch
                    .Where(bt => bt.BranchTypeId == 2)
                    .AnyAsync(bt => bt.Id == dto.SecondaryBranchId);
            if (!isValidSecondaryBranchType)
            {
                return BadRequest(error: "Invalid Branch Type ID..");
            }

            var distribution = new Distribution_Operation
            {
                PrimaryBranchId = dto.PrimaryBranchId,
                SecondaryBranchId = dto.SecondaryBranchId,
                ProductId = dto.ProductId,
                Quantity = dto.quantity,
                Date = dto.date
            };
            _context.Distribution_Operation.Add(distribution);
            var remainingQuantityToUpdate = dto.quantity;
            var productionOperations = await _context.Production_Operation
                .Where(po => po.BranchId == dto.PrimaryBranchId && po.ProductId == dto.ProductId)
                .OrderBy(po => po.Date)
                .ToListAsync();
            foreach (var production in productionOperations)
            {
                if (remainingQuantityToUpdate <= 0) break;
                var quantityToUpdate = Math.Min(remainingQuantityToUpdate, production.RemainingQuantity);
                production.RemainingQuantity -= quantityToUpdate;
                remainingQuantityToUpdate -= quantityToUpdate;
            }
            await _context.SaveChangesAsync();
            return Ok("Products have been Moved to Distrubution Operation Level");
        }

      
    }
}
