using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace Application.Middlewares
{
    public sealed class ExceptionMiddleware(RequestDelegate _next, ILogger<ExceptionMiddleware> _logger)
    {
        public async Task InvokeAsync(HttpContext http)
        {
            try
            {
                await _next(http);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(http, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext http, Exception ex)
        {
            http.Response.ContentType = "application/json";
            http.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            var response = new
            {
#if DEBUG
                message = ex.Message,
                stackTrace = ex.StackTrace
#elif RELEASE
                message = ex.Message,
#endif
            };

            var result = JsonSerializer.Serialize(response);
            return http.Response.WriteAsync(result);
        }
    }
}