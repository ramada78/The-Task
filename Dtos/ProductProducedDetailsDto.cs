namespace ElkoodTask.Dtos
{
    public class ProductProducedDetailsDto
    {
        [MaxLength(length: 100)]
        public string ProductName { get; set; }
        public int TotalQuantity { get; set; }
    }
}
