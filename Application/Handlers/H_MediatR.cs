using Microsoft.Extensions.DependencyInjection;

namespace Application.Handlers
{
    public static class H_MediatR
    {
        public static IServiceCollection RegisterRequestHandlers(this IServiceCollection services)
        {
            return services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblies(typeof(H_MediatR).Assembly);
                config.NotificationPublisher = new TaskWhenAllPublisher();
            });
        }
    }
}