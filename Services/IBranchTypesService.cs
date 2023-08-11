namespace ElkoodTask.Servies
{
    public interface IBranchTypesService
    {
        Task<IEnumerable<BranchType>> GetAll();
        Task<BranchType> GetById(int id);
        Task<BranchType> Add(BranchType branchType);
        BranchType Update(BranchType branchType);
        BranchType Delete(BranchType branchType);
    }
}
