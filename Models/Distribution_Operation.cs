namespace ElkoodTask.Models
{
    public class Distribution_Operation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [InverseProperty("PrimaryBranch")]
        public int PrimaryBranchId { get; set; }
        public Branch PrimaryBranch { get; set; }
        [InverseProperty("SecondaryBranch")]
        public int SecondaryBranchId { get; set; }
        public Branch SecondaryBranch { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
    }
}
