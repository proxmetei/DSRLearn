using AutoMapper;
using DSRLearn.Context.Entities;
using DSRLearn.Context;
using Microsoft.EntityFrameworkCore;

namespace DSRLearn.Services.Payments
{
    public class PaymentModel
    {
        public Guid Id { get; set; }
        public Guid DebtId { get; set; }
        public int Amount { get; set; }
        public DateOnly Date { get; set; }
    }
    public class PaymentModelProfile : Profile
    {
        public PaymentModelProfile()
        {
            CreateMap<Payment, PaymentModel>()
                .BeforeMap<PaymentModelActions>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.DebtId, opt => opt.Ignore())
                ;
        }

        public class PaymentModelActions : IMappingAction<Payment, PaymentModel>
        {
            private readonly IDbContextFactory<MainDbContext> contextFactory;

            public PaymentModelActions(IDbContextFactory<MainDbContext> contextFactory)
            {
                this.contextFactory = contextFactory;
            }

            public void Process(Payment source, PaymentModel destination, ResolutionContext context)
            {
                using var db = contextFactory.CreateDbContext();

                var payment = db.Payments.Include(x => x.Debt).FirstOrDefault(x => x.Id == source.Id);

                destination.Id = payment.Uid;

                destination.DebtId = payment.Debt.Uid;
            }
        }
    }
}
