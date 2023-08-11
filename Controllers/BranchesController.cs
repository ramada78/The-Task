using ElkoodTask.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElkoodTask.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BranchesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var branches = await _context.Branch
                .Include(bi => bi.Company)
                .Select(pi => new BranchDetailsDto
                {
                    Id = pi.Id,
                    Name = pi.Name,
                    BranchTypeName = pi.BranchType.Name,
                    CompanyName = pi.Company.Name,
                    location = pi.Location
                })
                .ToListAsync();
            return Ok(branches);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] BranchDto dto)
        {
            var isValidBranchType = await _context.BranchType.AnyAsync(bi => bi.Id == dto.BranchTypeId);
            var isValidCompany = await _context.Company.AnyAsync(bi => bi.Id == dto.CompanyId);
            
            if (!isValidBranchType) 
            {
                return BadRequest(error: "Invalid Branch Type ID");
            }
            if (!isValidCompany)
            {
                return BadRequest(error: "Invalid Company ID");
            }

            var branch = new Branch
            {
                Name = dto.Name,
                BranchTypeId = dto.BranchTypeId,
                CompanyId = dto.CompanyId,
                Location = dto.location
            };
            await _context.AddAsync(branch);
            _context.SaveChanges();
            return Ok(branch);
        }
       
    }
}
