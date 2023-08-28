using Microsoft.EntityFrameworkCore;
using ProjectSMEHelper.API.DBContext.PostgreDBContext;

namespace ProjectSMEHelper.API.Helpers;
public class DAL
{
    private readonly PostgreDbContext _postgreContext;
    public DAL(PostgreDbContext postgreDbContext)
    {
        _postgreContext = postgreDbContext;
    }
    public async Task<string> GetEmailContainer()
    {
        var response = _postgreContext.EmailContainerConfiguration.FirstOrDefault(s => s.Status == 1 && s.Name == "Default");
        if (response == null)
            return "";
        return response.BodyTemplate;
    }
}
