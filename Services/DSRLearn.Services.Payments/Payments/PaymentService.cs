using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DSRLearn.Common.Exceptions;
using DSRLearn.Common.Validator;
using DSRLearn.Context;
using DSRLearn.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace DSRLearn.Services.Payments
{
    public class PaymentService: IPaymentService
    {
        private readonly IDbContextFactory<MainDbContext> dbContextFactory;
        private readonly IMapper mapper;
        private readonly IModelValidator<CreatePaymentModel> createModelValidator;
        public PaymentService(
            IDbContextFactory<MainDbContext> dbContextFactory,
            IMapper mapper,
            IModelValidator<CreatePaymentModel> createModelValidator
            )
        {
            this.dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            this.createModelValidator = createModelValidator;
        }
        public async Task<IEnumerable<PaymentModel>> GetAll()
        {
            using var context = await dbContextFactory.CreateDbContextAsync();
            var debts = await context.Payments
                .Include(x => x.Debt)
                .ToArrayAsync();

            var result = mapper.Map<IEnumerable<PaymentModel>>(debts);

            return result;
        }
        public async Task<IEnumerable<PaymentModel>> GetByDebtId(Guid debtId)
        {
            using var context = await dbContextFactory.CreateDbContextAsync();
            var debts = await context.Payments
                .Include(x => x.Debt)
                .Where(x => x.Debt.Uid == debtId)
                .ToArrayAsync();

            var result = mapper.Map<IEnumerable<PaymentModel>>(debts);

            return result;
        }
        public async Task<PaymentModel> GetById(Guid id)
        {
            using var context = await dbContextFactory.CreateDbContextAsync();
            var debt = await context.Payments
                .Include(x => x.Debt)
                .FirstOrDefaultAsync(x => x.Uid == id);

            var result = mapper.Map<PaymentModel>(debt);

            return result;
        }
        public async Task<PaymentModel> Create(CreatePaymentModel model)
        {
            await createModelValidator.CheckAsync(model);

            using var context = await dbContextFactory.CreateDbContextAsync();

            var payment = mapper.Map<Payment>(model);

            var debt = await context.Debts.Where(x => x.Uid == model.DebtId).FirstOrDefaultAsync();

            debt.Amount -= model.Amount;

            context.Debts.Update(debt);

            await context.Payments.AddAsync(payment);

            await context.SaveChangesAsync();

            return mapper.Map<PaymentModel>(payment);
        }
        public async Task Delete(Guid id) 
        {
            using var context = await dbContextFactory.CreateDbContextAsync();

            var payment = await context.Payments.Where(x => x.Uid == id).FirstOrDefaultAsync();

            if (payment == null)
                throw new ProcessException($"Payment (ID = {id}) not found.");

            context.Payments.Remove(payment);

            await context.SaveChangesAsync();
        }
    }
}
