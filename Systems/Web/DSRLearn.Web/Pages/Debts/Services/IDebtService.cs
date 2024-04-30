using DSRLearn.Web.Pages.Debts.Models;
using DSRLearn.Web.Pages.Payments.Models;

namespace DSRLearn.Web.Pages.Debts.Services
{
    public interface IDebtService
    {
        Task<IEnumerable<DebtModel>> GetAll();
        Task<DebtModel> GetDebt(Guid debtId);
        Task AddDebt(CreateDebtModel model);
        Task EditDebt(UpdateDebtModel model);
        Task DeleteDebt(Guid debtId);
        Task NettingDebt(CreateNettingModel model);
        Task<IEnumerable<DebtModel>> GetCredits();
        Task<IEnumerable<DebtModel>> GetDebts();
    }
}
