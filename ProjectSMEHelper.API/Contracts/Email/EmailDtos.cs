namespace ProjectSMEHelper.API.Contracts.Email;

public class EmailDtos
{
    public string? Receiver { get; set; }
    public string? Subject { get; set; }
    public string? Body { get; set; }
    public string? Fullname { get; set; } = "";
}
