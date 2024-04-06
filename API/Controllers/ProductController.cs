using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[ApiVersion(1)]
	[ApiVersion(2)]
	[Route("api/v{v:apiVersion}/[controller]")]
	[ApiController]
	public sealed class ProductController(IMediator _mediator) : ControllerBase
	{
	}
}