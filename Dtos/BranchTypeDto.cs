namespace ElkoodTask.Dtos
{
    public class BranchTypeDto
    {
        [MaxLength(length: 100)]
        public string Name { get; set; }
    }
}
