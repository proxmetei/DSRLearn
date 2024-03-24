namespace DSRLearn.Services.Debts;

using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddDebtService(this IServiceCollection services)
    {
        return services
            .AddSingleton<IDebtService, DebtService>();
    }
}