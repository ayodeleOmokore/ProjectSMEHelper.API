using AutoMapper;
using ProjectSMEHelper.API.Contracts.Company.Requests;
using ProjectSMEHelper.API.Models.Company;

namespace ProjectSMEHelper.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() {
            CreateMap<Industry, IndustryResponseDTOs>().ReverseMap();
        }
    }
}
