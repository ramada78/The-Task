namespace ElkoodTask.Models
{
    public class Production_Operation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int BranchId { get; set; }
        public Branch Branch { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
        public int RemainingQuantity { get; set; }
        public DateTime Date { get; set; }
        public Production_Operation()
        {
            RemainingQuantity = Quantity;
        }
    }
}
