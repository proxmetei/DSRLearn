namespace DSRLearn.Api;

using DSRLearn.Services.Settings;
public static class Bootstrapper
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services
            .AddMainSettings()
            .AddSwaggerSettings()
            .AddLogSettings();

        return services;
    }
}
