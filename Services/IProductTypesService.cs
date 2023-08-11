namespace ElkoodTask.Servies
{
    public interface IProductTypesService
    {
        Task<IEnumerable<ProductType>> GetAll();
        Task<ProductType> GetById(int id);
        Task<ProductType> Add(ProductType productType);
        ProductType Update(ProductType productType);
        ProductType Delete(ProductType productType);
    }
}
