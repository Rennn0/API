using Application.Commands;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiVersion(1)]
    [ApiVersion(2)]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class TestController(IMediator _mediator) : ControllerBase
    {
        [HttpPost, MapToApiVersion(1)]
        public async Task<IActionResult> GetV1(C_Test c_Test)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string result = await _mediator.Send(c_Test);
            return Ok($"{result} version 1");
        }

        [HttpGet("t2"), MapToApiVersion(1)]
        public async Task<IActionResult> GetT2([FromQuery] C_Test2 command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet("t3"), MapToApiVersion(1)]
        public async Task<IActionResult> GetT3([FromQuery] C_Test3 command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost, MapToApiVersion(2)]
        public async Task<IActionResult> GetV2(C_Test c_Test)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string result = await _mediator.Send(c_Test);
            return Ok($"{result} version 2");
        }
    }
}