namespace ElkoodTask.Dtos
{
    public class ProductionOprerationDto
    {
        public int Id { get; set; }
        public int BranchId { get; set; }
        public int ProductId { get; set; }
        public int quantity { get; set; }
        public DateTime date { get; set; }
    }
}
