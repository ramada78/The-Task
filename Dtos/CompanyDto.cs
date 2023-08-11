namespace ElkoodTask.Dtos
{
    public class CompanyDto
    {
        [MaxLength(length: 100)]
        public string Name { get; set; }

        [MaxLength(length: 100)]
        public string location { get; set; }
        public DateTime establishmentDate { get; set; }

        [MaxLength(length: 100)]
        public string activity { get; set; }
    }
}
