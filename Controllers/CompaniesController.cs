using ElkoodTask.Servies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElkoodTask.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompaniesService companiesService;

        public CompaniesController(ICompaniesService companiesService)
        {
            this.companiesService = companiesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var Company = await companiesService.GetAll();
            return Ok(Company);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CompanyDto dto)
        {
            var company = new Company { 
                Name = dto.Name, 
                Activity = dto.activity,
                EstablishmentDate = dto.establishmentDate,
                Location = dto.location
            };
            await companiesService.Add(company);
            return Ok(company);
        }
        
    }
}
