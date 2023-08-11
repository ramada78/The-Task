using ElkoodTask.Models;
using Microsoft.EntityFrameworkCore;

namespace ElkoodTask.Servies
{
    public class ProductTypesService : IProductTypesService
    {
        private readonly ApplicationDbContext _context;

        public ProductTypesService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ProductType> Add(ProductType productType)
        {
            await _context.AddAsync(productType);
            _context.SaveChanges();
            return productType;
        }

        public ProductType Delete(ProductType productType)
        {
            _context.Remove(productType);
            _context.SaveChanges();
            return productType;
        }

        public async Task<IEnumerable<ProductType>> GetAll()
        {
            return await _context.ProductType.ToListAsync();
        }

        public async Task<ProductType> GetById(int id)
        {
            return await _context.ProductType.SingleOrDefaultAsync(pt => pt.Id == id);
        }

        public ProductType Update(ProductType productType)
        {
            _context.Update(productType);
            _context.SaveChanges();
            return productType;
        }
    }
}
