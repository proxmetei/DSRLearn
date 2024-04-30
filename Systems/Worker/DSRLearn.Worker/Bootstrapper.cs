﻿namespace DSRLearn.Worker;

using DSRLearn.Services.RabbitMq;
using Microsoft.Extensions.DependencyInjection;
using DSRLearn.Services.Logger;

public static class Bootstrapper
{
    public static IServiceCollection RegisterAppServices(this IServiceCollection services)
    {
        services
            .AddAppLogger()
            .AddRabbitMq()            
            ;

        services.AddSingleton<ITaskExecutor, TaskExecutor>();

        return services;
    }
}