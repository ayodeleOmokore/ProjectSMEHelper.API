using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectSMEHelper.API.Contracts.Company.Requests;
using ProjectSMEHelper.API.DBContext.PostgreDBContext;
using ProjectSMEHelper.API.Models.Company;

namespace ProjectSMEHelper.API.Services.CompanyServices
{
    public class CompanyService : ICompanyService
    {
        private readonly PostgreDbContext _postgreDB;
        private readonly Helpers.Utility _utility;
        private readonly IMapper _mapper;
        public CompanyService(PostgreDbContext postgreDbContext, Helpers.Utility utility, IMapper mapper)
        {
            _postgreDB = postgreDbContext;
            _utility = utility;
            _mapper = mapper;

        }

        public async Task<IEnumerable<IndustryResponseDTOs>> GetAllIndustries()
        {
            IEnumerable<Industry> industryList = _postgreDB.Industry.AsNoTracking().Where(x => x.Status == 1).OrderBy(x=>x.IndustryName);
            var response = _mapper.Map<IEnumerable<IndustryResponseDTOs>>(industryList);
            return response;
        }
    }
}
