using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nowadays.Api.Filters;
using Nowadays.Core.Dtos;
using Nowadays.Core.Models;
using Nowadays.Core.Services;

namespace Nowadays.Api.Controllers
{
    public class ProjectsController : BaseController
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResult(await _projectService.GetAllAsync());
        }

        [HttpGet("{id}")]
        [ServiceFilter(typeof(NotFoundFilter<Project, ProjectDto>))]
        public async Task<IActionResult> GetById(int id)
        {
            return CreateActionResult(await _projectService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProjectDto projectDto)
        {
            return CreateActionResult(await _projectService.AddAsync(projectDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProjectDto projectDto)
        {
            return CreateActionResult(await _projectService.UpdateAsync(projectDto));
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(NotFoundFilter<Project, ProjectDto>))]
        public async Task<IActionResult> Remove(int id)
        {
            return CreateActionResult(await _projectService.RemoveAsync(id));
        }

    }
}
