//using Application.OtherUtils;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Owin;
//using System.Security.Claims;

//namespace Application.Middlewares
//{
//	public sealed class TokenValidationMiddleware : OwinMiddleware
//	{
//		private TokenValidationMiddleware(OwinMiddleware _next) : base(_next)
//		{
//		}

//		public override Task Invoke(IOwinContext context)
//		{
//			string token = context.Request.Headers.Get("Authorization");
//			if (token != null)
//			{
//				try
//				{
//					Token tokenData = Security.Decode(token);
//					var identity = new ClaimsIdentity(;
//					context.Request.User = new ClaimsPrincipal();
//				}
//				catch
//				{
//				}
//			}
//		}

//		public async Task InvokeAsync(HttpContext context)
//		{
//			string? authHeader = context.Request.Headers["Authorization"];
//			if (authHeader == null || !authHeader.StartsWith("Bearer "))
//				throw new ArgumentException("No token");

//			string token = authHeader["Bearer ".Length..].Trim();
//			try
//			{
//				Token tokenData = Security.Decode(token);
//				context.Items["TokenData"] = tokenData;
//			}
//			catch
//			{
//				context.Response.StatusCode = 401;
//				return;
//			}

//			await _next(context);
//		}
//	}
//}