using Application.Commands.Category;
using Application.Commands.CategoryCommands;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Repository.DTOs;

namespace API.Controllers
{
	[ApiVersion(1)]
	[ApiVersion(2)]
	[Route("api/v{v:apiVersion}/[controller]")]
	[ApiController]
	public sealed class CategoryController(IMediator _mediator) : ControllerBase
	{
		[MapToApiVersion(1)]
		[HttpPost("add")]
		public async Task<CategoryDto> AddCategory([FromBody] C_AddCategory command)
		{
			return await _mediator.Send(command);
		}

		[MapToApiVersion(1)]
		[HttpPost("get")]
		public async Task<CategoryDto> GetCategory([FromBody] C_GetCategory command)
		{
			return await _mediator.Send(command);
		}

		[MapToApiVersion(1)]
		[HttpPost("update")]
		public async Task<CategoryDto> UpdateCategory([FromBody] C_UpdateCategory command)
		{
			return await _mediator.Send(command);
		}

		[MapToApiVersion(1)]
		[HttpPost("delete")]
		public async Task<Object> DeleteCategory([FromBody] C_DeleteCategory command)
		{
			return await _mediator.Send(command);
		}
	}
}