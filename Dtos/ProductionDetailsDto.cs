namespace ElkoodTask.Dtos
{
    public class ProductionDetailsDto
    {
        public int Id { get; set; }
        public string BranchName { get; set; }
        public string ProductName { get; set; }
        public int quantity { get; set; }
        public int RemainingQuantity { get; internal set; }
        public DateTime date { get; set; }
    }
}
