namespace DSRLearn.Web;

using DSRLearn.Web.Handler;
using DSRLearn.Web.Pages.Auth.Services;
using DSRLearn.Web.Pages.Debts.Services;
using DSRLearn.Web.Pages.Payments.Services;
using DSRLearn.Web.Pages.Users.Services;
using DSRLearn.Web.Providers;
using DSRLearn.Web.Services;
using Microsoft.AspNetCore.Components.Authorization;

public static class Bootstrapper
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration = null)
    {
        services
            .AddScoped<IConfigurationService, ConfigurationService>()
            .AddScoped<IDebtService, DebtService>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<IPaymentService, PaymentService>()
            .AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>()
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<DelegatingHandler, AuthenticationHandler>();

        return services;
    }
}
