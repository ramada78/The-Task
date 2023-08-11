namespace ElkoodTask.Dtos
{
    public class ProductDto
    {
        [MaxLength(length: 100)]
        public string Name { get; set; }
        public int ProductTypeId { get; set; }
    }
}
