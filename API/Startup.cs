using Application.Handlers;
using Application.Middlewares;
using Asp.Versioning;
using Domain.Entities;
using MediatR;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Repository.Base;
using Serilog;
using System.Text.Json.Serialization;

namespace API
{
	public sealed class Startup(IConfiguration _configuration)
	{
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseMiddleware<ExceptionMiddleware>();
			app.UseMiddleware<UserDataCollector>();

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
				c.SwaggerEndpoint("/swagger/v2/swagger.json", "V2");
			});

			app.UseRouting()
			   .UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}

		public void ConfigureServices(IServiceCollection services)
		{
			string logFolderDay = DateTime.Now.ToString("MM-dd");
			string logFileHour = DateTime.Now.Hour.ToString();
			Log.Logger = new LoggerConfiguration()
				.WriteTo.Console()
				.WriteTo.File($"__logs__/date-{logFolderDay}/hour-{logFileHour}-.txt", rollingInterval: RollingInterval.Hour)
				.CreateLogger();

			services.AddLogging(builder =>
			{
				builder.AddSerilog(dispose: true);
			});

			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "v1 api", Version = "1" });
				c.SwaggerDoc("v2", new OpenApiInfo { Title = "v2 api", Version = "2" });
			});

			/*  MediatR dependency injections goes here  */

			services.RegisterRequestHandlers();
			//services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TracingBehavior<,>));
			//services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));

			/*-------------------------------------------------------------------------------*/

			services.AddDbContext<EShopContext>(opt =>
			{
				opt.UseSqlServer(_configuration.GetConnectionString("EShop"))
				   .EnableSensitiveDataLogging();
			});
			services.AddTransient<IUnitOfWork, UnitOfWork>();
			services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));

			services.AddApiVersioning(options =>
			{
				options.DefaultApiVersion = new ApiVersion(1);
				options.ReportApiVersions = true;
				options.AssumeDefaultVersionWhenUnspecified = true;
				options.ApiVersionReader = new UrlSegmentApiVersionReader();
			}).AddApiExplorer(options =>
			{
				options.GroupNameFormat = "'v'V";
				options.SubstituteApiVersionInUrl = true;
			});
		}
	}
}