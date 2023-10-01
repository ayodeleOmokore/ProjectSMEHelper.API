namespace ProjectSMEHelper.API.Contracts.Company.Requests
{
    public class IndustryResponseDTOs
    {
        public Guid Id { get; set; }
        public string IndustryName { get; set; }
        public int Status { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
