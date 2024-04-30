namespace DSRLearn.Api;

using DSRLearn.Services.Settings;
using DSRLearn.Services.Logger;
using DSRLearn.Services.Debts;
using DSRLearn.Services.Payments;
using DSRLearn.Services.UserAccount;
using DSRLearn.Services.RabbitMq;
using DSRLearn.Services.Actions;
using DSRLearn.Services.HostedMessage;

public static class Bootstrapper
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration = null)
    {
        services
            .AddMainSettings()
            .AddSwaggerSettings()
            .AddIdentitySettings()
            .AddLogSettings()
            .AddAppLogger()
            .AddDebtService()
            .AddPaymentService()
            .AddUserAccountService()
            .AddActions()
            .AddRabbitMq()
            .AddUserHostedMessageService();

        return services;
    }
}
