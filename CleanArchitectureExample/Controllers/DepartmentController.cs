using CleanArchitectureExample.Application.DTOs;
using CleanArchitectureExample.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static CleanArchitectureExample.Application.Features.Queries.DepartmentQuery;

namespace CleanArchitectureExample.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController:ControllerBase
    {
        private readonly IMediator _mediator;
        public DepartmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetListDepartment")]
        public async Task<IActionResult> GetListDepartment()
        {
            List<DepartmentDTO> product = await _mediator.Send(new GetListDepartment());
            return product is not null ? Ok(product) : NotFound();
        }

        [HttpGet("GetDepartmentById")]
        public async Task<IActionResult> GetDepartmentById(int Id)
        {
            DepartmentDTO product = await _mediator.Send(new GetDepartmentById(Id));
            return product is not null ? Ok(product) : NotFound();
        }
    }
}
