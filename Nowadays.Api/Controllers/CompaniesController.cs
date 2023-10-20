using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nowadays.Api.Filters;
using Nowadays.Core.Dtos;
using Nowadays.Core.Models;
using Nowadays.Core.Services;

namespace Nowadays.Api.Controllers
{
    public class CompaniesController : BaseController
    {
        private readonly ICompanyService _companyService;

        public CompaniesController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResult(await _companyService.GetAllAsync());
        }

        [HttpGet("{id}")]
        [ServiceFilter(typeof(NotFoundFilter<Company, CompanyDto>))]
        public async Task<IActionResult> GetById(int id)
        {
            return CreateActionResult(await _companyService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Save(CompanyDto companyDto)
        {
            return CreateActionResult(await _companyService.AddAsync(companyDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(CompanyDto companyDto)
        {
            return CreateActionResult(await _companyService.UpdateAsync(companyDto));
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(NotFoundFilter<Company, CompanyDto>))]
        public async Task<IActionResult> Remove(int id)
        {
            return CreateActionResult(await _companyService.RemoveAsync(id));
        }

    }
}
