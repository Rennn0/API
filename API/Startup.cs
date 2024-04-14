using Application.Handlers;
using Application.Interfaces;
using Application.Middlewares;
using Application.OtherUtils;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Update.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository.Base;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

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

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
				c.SwaggerEndpoint("/swagger/v2/swagger.json", "V2");
			});

			app.UseMiddleware<ExceptionMiddleware>();
			app.UseMiddleware<UserDataCollector>();

			app.UseRouting();

			app.UseHttpsRedirection();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
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

			services.AddHttpContextAccessor();
			services.AddAuthorization();

			services.AddDbContext<EShopContext>(opt =>
			{
				opt.UseSqlServer(_configuration.GetConnectionString("EShop"))
				   .EnableSensitiveDataLogging();
			});

			services.Configure<Smtp>(_configuration.GetSection("Smtp"));
			services.Configure<JwtConfig>(_configuration.GetSection("Jwt"));
			services.Configure<Application.OtherUtils.Domain>(_configuration.GetSection("Domain"));

			/*-------------------------------------------------------------------------------*/
			/*---------------------------------DEPENDENCY------------------------------------*/
			/*-------------------------------------------------------------------------------*/

			services.AddSingleton<ISmtp, Smtp>();

			services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));

			services.AddTransient<IUnitOfWork, UnitOfWork>();
			services.AddTransient<IPostman, Postman>();

			/*-------------------------------------------------------------------------------*/
			/*---------------------------------MEDIATR---------------------------------------*/
			/*-------------------------------------------------------------------------------*/

			services.RegisterRequestHandlers();
			//services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TracingBehavior<,>));
			//services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));

			/*-------------------------------------------------------------------------------*/
			/*-------------------------------------------------------------------------------*/
			/*-------------------------------------------------------------------------------*/

			string? key = _configuration.GetSection("Jwt:Key").Get<string>();
			string? issuuer = _configuration.GetSection("Jwt:Issuer").Get<string>();
			string? audience = _configuration.GetSection("Jwt:Audience").Get<string>();

			if (key.IsNullOrEmpty() || issuuer.IsNullOrEmpty() || audience.IsNullOrEmpty())
				throw new ArgumentException("jwt config not provided");

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = issuuer,
						ValidAudience = audience,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!)),
						ClockSkew = TimeSpan.Zero
					};
				});

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