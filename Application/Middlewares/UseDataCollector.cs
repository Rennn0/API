using Application.OtherUtils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Middlewares
{
	public sealed class UserDataCollector(RequestDelegate _next, ILogger<ExceptionMiddleware> _logger)
	{
		public async Task InvokeAsync(HttpContext http)
		{
			string ipAdress = http.Connection.RemoteIpAddress.MapToIPv4().ToString();
			_logger.LogInformation($"Client IP: {ipAdress}");
			_logger.LogInformation($"Header size = {ObjectSize.Get(http.Request.Headers)} bytes");
			_logger.LogInformation($"Query size = {ObjectSize.Get(http.Request.Query)} bytes");

			await _next(http);
		}
	}
}