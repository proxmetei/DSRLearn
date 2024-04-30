using DSRLearn.Web.Pages.Payments.Models;

namespace DSRLearn.Web.Pages.Payments.Services
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentModel>> GetByDebt(Guid debtId);
        Task AddPayment(CreatePaymentModel model);
    }
}
