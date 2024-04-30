using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSRLearn.Services.Payments
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentModel>> GetAll();
        Task<IEnumerable<PaymentModel>> GetByDebtId(Guid debtId);
        Task<PaymentModel> GetById(Guid id);
        Task<PaymentModel> Create(CreatePaymentModel model);
        Task Delete(Guid id);
    }
}
