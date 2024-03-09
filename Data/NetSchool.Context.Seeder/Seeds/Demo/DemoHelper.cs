namespace DSRLearn.Context.Seeder;

using DSRLearn.Context.Entities;

public class DemoHelper
{
    public IEnumerable<Debt> GetDebts = new List<Debt>
    {
        new Debt()
        {
            Uid = Guid.NewGuid(),
            Amount = 10000,
            RepaidDate = new DateOnly(2024,5,9),
            Creditor = new User()
            {
                Uid = Guid.NewGuid(),
                Login = "prox",  
                Detail = new UserDetail()
                {
                    Name = "Алексей",
                    Surname = "Корчагин",
                    Patronymic = "Павлович"
                }
            },
            Debtor = new User()
            {
                Uid = Guid.NewGuid(),
                Login = "metei",
                Detail = new UserDetail()
                {
                    Name = "Михаил",
                    Surname = "Лобов",
                    Patronymic = "Игоревич"
                }
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
}