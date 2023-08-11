namespace ElkoodTask.Dtos
{
    public class DistrubutionDetailsDto
    {
        public int Id { get; set; }
        public string PrimaryBranchName { get; set; }
        public string SecondaryBranchName { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
    }
}
