namespace DSRLearn.Api;

using DSRLearn.Services.Settings;
using DSRLearn.Services.Logger;
using DSRLearn.Services.Debts;
public static class Bootstrapper
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration = null)
    {
        services
            .AddMainSettings()
            .AddSwaggerSettings()
            .AddLogSettings()
            .AddAppLogger()
            .AddDebtService();

        return services;
    }
}
