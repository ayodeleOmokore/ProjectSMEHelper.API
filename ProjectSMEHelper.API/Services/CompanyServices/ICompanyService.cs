using ProjectSMEHelper.API.Contracts.Company.Requests;

namespace ProjectSMEHelper.API.Services.CompanyServices
{
    public interface ICompanyService
    {
        Task<IEnumerable<IndustryResponseDTOs>> GetAllIndustries();
    }
}
