namespace ElkoodTask.Dtos
{
    public class ProductTypeDto
    {

        [MaxLength(length: 100)]
        public string Name { get; set; }
    }
}
