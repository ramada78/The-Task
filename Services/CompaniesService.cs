using ElkoodTask.Models;
using Microsoft.EntityFrameworkCore;

namespace ElkoodTask.Servies
{
    public class CompaniesService : ICompaniesService
    {
        private readonly ApplicationDbContext _context;
        public CompaniesService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Company> Add(Company company)
        {
            await _context.AddAsync(company);
            _context.SaveChanges();
            return company;
        }

        public Company Delete(Company company)
        {
            _context.Remove(company);
            _context.SaveChanges();
            return company;
        }

        public async Task<IEnumerable<Company>> GetAll()
        {
            return await _context.Company.ToListAsync();
        }

        public async Task<Company> GetById(int id)
        {
            return await _context.Company.SingleOrDefaultAsync(ci => ci.Id == id);
        }

        public Company Update(Company company)
        {
            _context.Update(company);
            _context.SaveChanges();
            return company;
        }
    }
}
