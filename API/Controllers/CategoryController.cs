using Application.Commands.Category;
using Application.Commands.CategoryCommands;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.DTOs;

namespace API.Controllers
{
	[Authorize]
	[ApiVersion(1)]
	[ApiVersion(2)]
	[Route("api/v{v:apiVersion}/[controller]")]
	[ApiController]
	public sealed class CategoryController(IMediator _mediator) : ControllerBase
	{
		[MapToApiVersion(1)]
		[HttpPost("Add")]
		public async Task<CategoryDto> AddCategory([FromBody] C_AddCategory command)
		{
			return await _mediator.Send(command);
		}

		[MapToApiVersion(1)]
		[HttpPost("Get")]
		public async Task<CategoryDto> GetCategory([FromBody] C_GetCategory command)
		{
			return await _mediator.Send(command);
		}

		[MapToApiVersion(1)]
		[HttpPost("Update")]
		public async Task<CategoryDto> UpdateCategory([FromBody] C_UpdateCategory command)
		{
			return await _mediator.Send(command);
		}

		[MapToApiVersion(1)]
		[HttpPost("Delete")]
		public async Task<object> DeleteCategory([FromBody] C_DeleteCategory command)
		{
			return await _mediator.Send(command);
		}
	}
}