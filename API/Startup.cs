using Application.Handlers;
using Application.Middlewares;
using Asp.Versioning;
using MediatR;
using Microsoft.OpenApi.Models;

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

            app.UseRouting()
               .UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "v1 api", Version = "1" });
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "v2 api", Version = "2" });
            });

            services.RegisterRequestHandlers();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TracingBehavior<,>));
            services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1);
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
                .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });
        }
    }
}