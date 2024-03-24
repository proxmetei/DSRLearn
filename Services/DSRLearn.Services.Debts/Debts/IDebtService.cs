namespace DSRLearn.Services.Debts
{
    public interface IDebtService
    {
        Task<IEnumerable<DebtModel>> GetAll();
        Task<IEnumerable<DebtModel>> GetByDebtorId(Guid debtorId);
        Task<IEnumerable<DebtModel>> GetByCreditorId(Guid creditorId);
        Task<DebtModel> GetById(Guid id);
        Task<DebtModel> Create(CreateDebtModel model);
        Task Update(Guid id, UpdateDebtModel model);
        Task Delete(Guid id);
    }
}
