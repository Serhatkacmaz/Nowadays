using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nowadays.Api.Filters;
using Nowadays.Core.Dtos;
using Nowadays.Core.Models;
using Nowadays.Core.Services;

namespace Nowadays.Api.Controllers
{
    public class IssuesController : BaseController
    {
        private readonly IIssueService _issueService;

        public IssuesController(IIssueService issueService)
        {
            _issueService = issueService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResult(await _issueService.GetAllAsync());
        }

        [HttpGet("{id}")]
        [ServiceFilter(typeof(NotFoundFilter<Issue, IssueDto>))]
        public async Task<IActionResult> GetById(int id)
        {
            return CreateActionResult(await _issueService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Save(IssueDto issueDto)
        {
            return CreateActionResult(await _issueService.AddAsync(issueDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(IssueDto issueDto)
        {
            return CreateActionResult(await _issueService.UpdateAsync(issueDto));
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(NotFoundFilter<Issue, IssueDto>))]
        public async Task<IActionResult> Remove(int id)
        {
            return CreateActionResult(await _issueService.RemoveAsync(id));
        }

    }
}
