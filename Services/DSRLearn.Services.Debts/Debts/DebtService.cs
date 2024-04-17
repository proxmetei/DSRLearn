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
using static System.Reflection.Metadata.BlobBuilder;

namespace DSRLearn.Services.Debts
{
    public class DebtService : IDebtService
    {
        private readonly IDbContextFactory<MainDbContext> dbContextFactory;
        private readonly IMapper mapper;
        private readonly IModelValidator<CreateDebtModel> createModelValidator;
        private readonly IModelValidator<UpdateDebtModel> updateModelValidator;
        public DebtService(
            IDbContextFactory<MainDbContext> dbContextFactory,
            IMapper mapper,
            IModelValidator<CreateDebtModel> createModelValidator,
            IModelValidator<UpdateDebtModel> updateModelValidator
            )
        {
            this.dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            this.createModelValidator = createModelValidator;
            this.updateModelValidator = updateModelValidator;
        }
        public async Task<IEnumerable<DebtModel>> GetByDebtorId(Guid debtorId)
        {
            using var context = await dbContextFactory.CreateDbContextAsync();
            var debts = await context.Debts
                .Include(x => x.Creditor)
                .Include(x => x.Debtor)
                .Where(x => x.Debtor.Id == debtorId)
                .ToArrayAsync();

            var result = mapper.Map<IEnumerable<DebtModel>>(debts);

            return result;
        }
        public async Task<IEnumerable<DebtModel>> GetByCreditorId(Guid creditorId)
        {
            using var context = await dbContextFactory.CreateDbContextAsync();
            var debts = await context.Debts
                .Include(x => x.Debtor)
                .Include(x => x.Creditor)
                .Where(x => x.Creditor.Id == creditorId)
                .ToArrayAsync();

            var result = mapper.Map<IEnumerable<DebtModel>>(debts);

            return result;
        }
        public async Task<DebtModel> GetById(Guid id)
        {
            using var context = await dbContextFactory.CreateDbContextAsync();
            var debt = await context.Debts
                .Include(x => x.Debtor)
                .Include(x => x.Creditor)
                .FirstOrDefaultAsync(x => x.Uid == id);

            var result = mapper.Map<DebtModel>(debt);

            return result;
        }
        public async Task<IEnumerable<DebtModel>> GetAll()
        {
            using var context = await dbContextFactory.CreateDbContextAsync();
            var debts = await context.Debts
                .Include(x => x.Debtor)
                .Include(x => x.Creditor)
                .ToArrayAsync();

            var result = mapper.Map<IEnumerable<DebtModel>>(debts);

            return result;
        }
        public async Task<DebtModel> Create(CreateDebtModel model)
        {
            await createModelValidator.CheckAsync(model);

            using var context = await dbContextFactory.CreateDbContextAsync();

            var debt = mapper.Map<Debt>(model);

            await context.Debts.AddAsync(debt);

            await context.SaveChangesAsync();

            return mapper.Map<DebtModel>(debt);
        }
        public async Task Update(Guid id, UpdateDebtModel model)
        {
            await updateModelValidator.CheckAsync(model);

            using var context = await dbContextFactory.CreateDbContextAsync();

            var debt = await context.Debts.Where(x => x.Uid == id).FirstOrDefaultAsync();

            debt = mapper.Map(model, debt);

            context.Debts.Update(debt);

            await context.SaveChangesAsync();
        }
        public async Task Delete(Guid id)
        {
            using var context = await dbContextFactory.CreateDbContextAsync();

            var debt = await context.Debts.Where(x => x.Uid == id).FirstOrDefaultAsync();

            if (debt == null)
                throw new ProcessException($"Debt (ID = {id}) not found.");

            context.Debts.Remove(debt);

            await context.SaveChangesAsync();
        }
    }
}
