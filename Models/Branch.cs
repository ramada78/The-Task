namespace ElkoodTask.Models
{
    public class Branch
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(length: 100)]
        public string Name { get; set; }
        public int BranchTypeId { get; set; }
        public BranchType BranchType { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }

        [MaxLength(length: 100)]
        public string Location { get; set; }

        public ICollection<Production_Operation> Production_Operation { get; set; }
        public ICollection<Distribution_Operation> Distribution_Operation { get; set; }

    }
}
