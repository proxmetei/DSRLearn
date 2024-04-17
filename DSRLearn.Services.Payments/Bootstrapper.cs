namespace DSRLearn.Services.Payments;

using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddPaymentService(this IServiceCollection services)
    {
        return services
            .AddSingleton<IPaymentService, PaymentService>();
    }
}