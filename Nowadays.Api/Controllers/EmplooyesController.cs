using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nowadays.Api.Filters;
using Nowadays.Core.Dtos;
using Nowadays.Core.Models;
using Nowadays.Core.Services;

namespace Nowadays.Api.Controllers
{
    public class EmplooyesController : BaseController
    {
        private readonly IEmployeeService _employeeService;

        public EmplooyesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResult(await _employeeService.GetAllAsync());
        }

        [HttpGet("{id}")]
        [ServiceFilter(typeof(NotFoundFilter<Employee, EmployeeDto>))]
        public async Task<IActionResult> GetById(int id)
        {
            return CreateActionResult(await _employeeService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Save(EmployeeDto employeeDto)
        {
            return CreateActionResult(await _employeeService.AddAsync(employeeDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(EmployeeDto employeeDto)
        {
            return CreateActionResult(await _employeeService.UpdateAsync(employeeDto));
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(NotFoundFilter<Employee, EmployeeDto>))]
        public async Task<IActionResult> Remove(int id)
        {
            return CreateActionResult(await _employeeService.RemoveAsync(id));
        }

    }
}
