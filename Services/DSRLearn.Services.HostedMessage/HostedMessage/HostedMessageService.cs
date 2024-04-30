using DSRLearn.Services.Actions;
using DSRLearn.Services.Debts;
using DSRLearn.Services.UserAccount;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DSRLearn.Services.HostedMessage
{
    public class HostedMessageService(IServiceScopeFactory serviceScopeFactory) : BackgroundService, IDisposable
    {
        private Timer? _timer = null;
        private readonly IAction action;
        private readonly IDebtService debtService;
        private const int expireDate = 5;

        public HostedMessageService(IAction action, IDebtService debtService, IServiceScopeFactory serviceScopeFactory): this(serviceScopeFactory)
        {
            this.action = action;

            this.debtService = debtService;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            using PeriodicTimer timer = new(TimeSpan.FromSeconds(60));

                while (await timer.WaitForNextTickAsync(stoppingToken))
                {
                    DoWork();
                }
        }
        private async Task DoWork()
        {
            var debts = await debtService.GetAll();

            using (IServiceScope scope = serviceScopeFactory.CreateScope())
            {
                IUserAccountService userAccountService =
                    scope.ServiceProvider.GetRequiredService<IUserAccountService>();

                foreach (var debt in debts)
                {
                    int timeDiff = debt.RepaidDate.DayNumber - DateOnly.FromDateTime(DateTime.Now).DayNumber;
                    if (timeDiff < expireDate)
                    {
                        var creditor = await userAccountService.GetById(debt.CreditorId);

                        var debtor = await userAccountService.GetById(debt.DebtorId);

                        action.SendMessage(new SendMeassageModel
                        {
                            Email = creditor.Email,
                            Subject = "Your credit",
                            Message = $"Срок выданного вами кредита (id {debt.Id}) пользователю {debtor.Email} истек или истекает {debt.RepaidDate}"
                        });

                        action.SendMessage(new SendMeassageModel
                        {
                            Email = debtor.Email,
                            Subject = "Your credit",
                            Message = $"Срок выданного вам кредита (id {debt.Id}) пользователем {creditor.Email} истек или истекает {debt.RepaidDate}"
                        });
                    }
                }
            }

        }

        public override void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
