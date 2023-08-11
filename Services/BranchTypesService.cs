using ElkoodTask.Models;
using Microsoft.EntityFrameworkCore;

namespace ElkoodTask.Servies
{
    public class BranchTypesService : IBranchTypesService
    {
        private readonly ApplicationDbContext _context;

        public BranchTypesService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<BranchType> Add(BranchType branchType)
        {
            await _context.AddAsync(branchType);
            _context.SaveChanges();
            return branchType;
        }

        public BranchType Delete(BranchType branchType)
        {
            _context.Remove(branchType);
            _context.SaveChanges();
            return branchType;
        }
        public async Task<IEnumerable<BranchType>> GetAll()
        {
            return await _context.BranchType.ToListAsync();
        }

        public async Task<BranchType> GetById(int id)
        {
            return await _context.BranchType.SingleOrDefaultAsync(bt => bt.Id == id);
        }

        public BranchType Update(BranchType branchType)
        {
            _context.Update(branchType);
            _context.SaveChanges();
            return branchType;
        }
    }
}