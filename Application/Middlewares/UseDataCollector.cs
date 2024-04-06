using Application.OtherUtils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Middlewares
{
	public sealed class UserDataCollector(RequestDelegate _next, ILogger<ExceptionMiddleware> _logger)
	{
		public async Task InvokeAsync(HttpContext http)
		{
			string ipAdress = http.Connection.RemoteIpAddress.ToString();
			_logger.LogInformation($"Client IP: {ipAdress}");
			_logger.LogInformation($"Header size = {ObjectSize.Get(http.Request.Headers)} bytes");
			_logger.LogInformation($"Query size = {ObjectSize.Get(http.Request.Query)} bytes");
			//string? userAgent = http.Request.Headers["user-agent"];
			//_logger.LogInformation($"User Agent:{userAgent}");

			//foreach (var header in http.Request.Headers)
			//{
			//    _logger.LogInformation($"Header: {header.Key} __ Value: {header.Value}");
			//}

			await _next(http);
		}
	}
}