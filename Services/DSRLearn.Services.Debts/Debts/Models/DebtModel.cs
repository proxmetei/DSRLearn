using AutoMapper;
using DSRLearn.Context;
using DSRLearn.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace DSRLearn.Services.Debts
{
    public class DebtModel
    {
        public Guid Id { get; set; }
        public Guid DebtorId { get; set; }
        public Guid CreditorId { get; set; }
        public int Amount { get; set; }
        public DateOnly RepaidDate { get; set; }
    }
    public class DebtModelProfile : Profile
    {
        public DebtModelProfile()
        {
            CreateMap<Debt, DebtModel>()
                .BeforeMap<DebtModelActions>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.DebtorId, opt => opt.Ignore())
                .ForMember(dest => dest.CreditorId, opt => opt.Ignore())
                ;
        }

        public class DebtModelActions : IMappingAction<Debt, DebtModel>
        {
            private readonly IDbContextFactory<MainDbContext> contextFactory;

            public DebtModelActions(IDbContextFactory<MainDbContext> contextFactory)
            {
                this.contextFactory = contextFactory;
            }

            public void Process(Debt source, DebtModel destination, ResolutionContext context)
            {
                using var db = contextFactory.CreateDbContext();

                var debt = db.Debts.Include(x => x.Creditor).Include(x => x.Debtor).FirstOrDefault(x => x.Id == source.Id);

                destination.Id = debt.Uid;

                destination.CreditorId = debt.Creditor.Id;

                destination.DebtorId = debt.Debtor.Id;
            }
        }
    }
}
