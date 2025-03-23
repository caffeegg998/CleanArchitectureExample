using CleanArchitectureExample.Application.Features.Commands;
using CleanArchitectureExample.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static CleanArchitectureExample.Application.Features.Commands.ProductCommand;
using static CleanArchitectureExample.Application.Features.Commands.RequestShippingCommand;
using static CleanArchitectureExample.Application.Features.Queries.RequestShippingQuery;

namespace CleanArchitectureExample.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestShippingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RequestShippingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRequestShipping([FromBody] CreateRequestShippingCommand command)
        {
            if (command == null)
            {
                return BadRequest("Dữ liệu yêu cầu không hợp lệ.");
            }

            try
            {
                var requestShipping = await _mediator.Send(command);

                if (requestShipping == null)
                {
                    return BadRequest("Không thể tạo yêu cầu vận chuyển.");
                }

                return Ok(requestShipping); // Trả về đối tượng RequestShipping đã tạo
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi hệ thống: {ex.Message}");
            }


        }
        [HttpGet]
        public async Task<ActionResult<List<RequestShipping>>> GetListRequestShipping()
        {
            List<RequestShipping> requestShippings = await _mediator.Send(new GetListRequestShipping());
            return requestShippings is not null ? Ok(requestShippings) : NotFound();
            
        }

        [HttpGet("get-request-shipping-by-market/{marketId}")]
        public async Task<IActionResult> GetByMarket(int marketId)
        {
            var result = await _mediator.Send(new GetRequestShippingByMarketQuery(marketId));
            return Ok(result);
        }
    }
}
