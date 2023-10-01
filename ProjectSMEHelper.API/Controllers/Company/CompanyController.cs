using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectSMEHelper.API.Services.CompanyServices;

namespace ProjectSMEHelper.API.Controllers.Company
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        [Authorize]
        [HttpGet("GetAllIndustries")]
        public async Task<IActionResult> GetAllIndustries()
        {
            return Ok(_companyService.GetAllIndustries());
        }
    }
}
