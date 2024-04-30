using Microsoft.Extensions.DependencyInjection;

namespace DSRLearn.Services.HostedMessage
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddUserHostedMessageService(this IServiceCollection services)
        {
            services.AddHostedService<HostedMessageService>();

            return services;
        }
    }
}
