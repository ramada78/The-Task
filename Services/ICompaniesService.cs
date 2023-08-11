namespace ElkoodTask.Servies
{
    public interface ICompaniesService
    {
        Task<IEnumerable<Company>> GetAll();
        Task<Company> GetById(int id);
        Task<Company> Add(Company company);
        Company Update(Company company);
        Company Delete(Company company);
    }
}
