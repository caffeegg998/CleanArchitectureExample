﻿using CleanArchitectureExample.Application.DTOs;
using CleanArchitectureExample.Application.Features.Commands;
using CleanArchitectureExample.Application.Features.Queries;
using CleanArchitectureExample.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static CleanArchitectureExample.Application.Features.Commands.ProductCommand;

namespace CleanArchitectureExample.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommand command)
        {
            var productId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetProductById), new { id = productId }, productId);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            ProductDTO product = await _mediator.Send(new GetProductByIdQuery(id));
            return product is not null ? Ok(product) : NotFound();
        }


        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("Product ID in the URL does not match the request body.");
            }

            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound("Product not found.");
            }

            return Ok("Product Updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _mediator.Send(new DeleteProductCommand(id));

            if (!result)
            {
                return NotFound("Product not found.");
            }

            return NoContent();
        }
    }
}
