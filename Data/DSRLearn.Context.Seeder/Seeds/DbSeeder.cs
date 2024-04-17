namespace DSRLearn.Context.Seeder;

using DSRLearn.Services.Debts;
using DSRLearn.Services.UserAccount;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
//using DSRLearn.Services.UserAccount;
using System;

public static class DbSeeder
{
    private static IServiceScope ServiceScope(IServiceProvider serviceProvider)
    {
        return serviceProvider.GetService<IServiceScopeFactory>()!.CreateScope();
    }

    private static MainDbContext DbContext(IServiceProvider serviceProvider)
    {
        return ServiceScope(serviceProvider)
            .ServiceProvider.GetRequiredService<IDbContextFactory<MainDbContext>>().CreateDbContext();
    }

    public static void Execute(IServiceProvider serviceProvider)
    {
        Task.Run(async () =>
            {
                await AddDemoUsers(serviceProvider);
                await AddDemoData(serviceProvider);
                //await AddAdministrator(serviceProvider);
            })
            .GetAwaiter()
            .GetResult();
    }

    private static async Task AddDemoData(IServiceProvider serviceProvider)
    {
        using var scope = ServiceScope(serviceProvider);
        if (scope == null)
            return;

        var settings = scope.ServiceProvider.GetService<DbSettings>();
        if (!(settings.Init?.AddDemoData ?? false))
            return;

        await using var context = DbContext(serviceProvider);

        if (await context.Debts.AnyAsync())
            return;

        var debts = new DemoHelper().GetDebts;
        foreach (var debt in debts)
        {
            var debtor =  context.Users.Where((x) => x.Email == debt.Debtor.Email).FirstOrDefault();
            var creditor = context.Users.Where((x) => x.Email == debt.Debtor.Email).FirstOrDefault();
            if (creditor == null || debtor == null)
                return;
            //debt.DebtorId = debtor.Id;
            //debt.CreditorId = creditor.Id;
            debt.Debtor = debtor;
            debt.Creditor = creditor;
        }

        await context.Debts.AddRangeAsync(debts);

        await context.SaveChangesAsync();
    }
        private static async Task AddDemoUsers(IServiceProvider serviceProvider)
    {
        using var scope = ServiceScope(serviceProvider);
        if (scope == null)
            return;

        var userAccountService = scope.ServiceProvider.GetService<IUserAccountService>();

        if (!(await userAccountService.IsEmpty()))
            return;
        foreach (var user in new DemoHelper().GetUsers)
        {
             await userAccountService.Create(user);
        }
    }
    private static async Task AddAdministrator(IServiceProvider serviceProvider)
    {
        //using var scope = ServiceScope(serviceProvider);
        //if (scope == null)
        //    return;

        //var settings = scope.ServiceProvider.GetService<DbSettings>();
        //if (!(settings.Init?.AddAdministrator ?? false))
        //    return;

        //var userAccountService = scope.ServiceProvider.GetService<IUserAccountService>();

        //if (!(await userAccountService.IsEmpty())) 
        //    return;

        //await userAccountService.Create(new RegisterUserAccountModel()
        //{
        //    Name = settings.Init.Administrator.Name,
        //    Email = settings.Init.Administrator.Email,
        //    Password = settings.Init.Administrator.Password,
        //});
    }
}