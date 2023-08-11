namespace ElkoodTask.Dtos
{
    public class BranchDto
    {
        [MaxLength(length: 100)]
        public string Name { get; set; }

        public int BranchTypeId { get; set; }

        public int CompanyId { get; set; }

        [MaxLength(length: 100)]
        public string location { get; set; }
    }
}
