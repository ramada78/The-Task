namespace ElkoodTask.Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(length: 100)]
        public string Name { get; set; }
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }

        public ICollection<Production_Operation> Production_Operation { get; set; }
        public ICollection<Distribution_Operation> Distribution_Operation { get; set; }
    }
}
