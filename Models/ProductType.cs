namespace ElkoodTask.Models
{
    public class ProductType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(length: 100)]
        public string Name { get; set; }
    }
}
