using ElkoodTask.Servies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElkoodTask.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BranchTypesController : ControllerBase
    {
        private readonly IBranchTypesService branchTypesService;

        public BranchTypesController(IBranchTypesService branchTypesService)
        {
            this.branchTypesService = branchTypesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var BranchTypes = await branchTypesService.GetAll();
            return Ok(BranchTypes);
        }

        [HttpPost]
        public async Task <IActionResult> CreateAsync(BranchTypeDto dto)
        {
            var branchType = new BranchType { Name = dto.Name};
            await branchTypesService.Add(branchType);
            return Ok(branchType); 
        }
        
    }
}
