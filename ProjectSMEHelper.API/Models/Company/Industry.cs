namespace ProjectSMEHelper.API.Models.Company
{
    public class Industry
    {
        public Guid Id { get; set; }
        public string IndustryName { get; set; }
        public int Status { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
