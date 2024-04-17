namespace DSRLearn.Context.Seeder;

using DSRLearn.Context.Entities;
using DSRLearn.Services.UserAccount;

public class DemoHelper
{
    public IEnumerable<Debt> GetDebts = new List<Debt>
    {
        new Debt()
        {
            Uid = Guid.NewGuid(),
            Amount = 10000,
            RepaidDate = new DateOnly(2024,5,9),
            Creditor = new User
            {
                Email = "aa@mail.ru"
            },
            Debtor = new User
            {
                Email = "bb@mail.ru"
            },
            Payments = new List<Payment>()
            {
                new Payment()
                {
                    Amount = 10000,
                    Date = new DateOnly(2024,2,25)
                },
                new Payment()
                {
                    Amount = 20000,
                    Date = new DateOnly(2024,3,7)
                }
            }
        },
    };
    public IEnumerable<RegisterUserAccountModel> GetUsers = new List<RegisterUserAccountModel>
    {
        new RegisterUserAccountModel()
        {
            Name = "Михаил",
            Surname = "Лобов",
            Email = "aa@mail.ru",
            Password = "pass1234"
        },
                new RegisterUserAccountModel()
        {
            Name = "Игорь",
            Surname = "Лукин",
            Email = "bb@mail.ru",
            Password = "pass1234"
        }
    };
}