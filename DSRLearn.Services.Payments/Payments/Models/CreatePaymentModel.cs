using AutoMapper;
using DSRLearn.Context.Entities;
using DSRLearn.Context;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DSRLearn.Services.Payments
{
    public class CreatePaymentModel
    {
        public Guid DebtId { get; set; }
        public int Amount { get; set; }
        public DateOnly Date { get; set; }
    }
    public class CreatePaymentModelProfile : Profile
    {
        public CreatePaymentModelProfile()
        {
            CreateMap<CreatePaymentModel, Payment>()
                .ForMember(dest => dest.DebtId, opt => opt.Ignore())
                .AfterMap<CreatePaymentModelActions>();
        }

        public class CreatePaymentModelActions : IMappingAction<CreatePaymentModel, Payment>
        {
            private readonly IDbContextFactory<MainDbContext> contextFactory;

            public CreatePaymentModelActions(IDbContextFactory<MainDbContext> contextFactory)
            {
                this.contextFactory = contextFactory;
            }

            public void Process(CreatePaymentModel source, Payment destination, ResolutionContext context)
            {
                using var db = contextFactory.CreateDbContext();

                var debt = db.Debts.FirstOrDefault(x => x.Uid == source.DebtId);

                destination.DebtId = debt.Id;
            }
        }
    }

    public class CreatePaymentModelValidator : AbstractValidator<CreatePaymentModel>
    {
        public CreatePaymentModelValidator(IDbContextFactory<MainDbContext> contextFactory)
        {
            RuleFor(x => x.Amount).
                GreaterThan(0).WithMessage("Amount of debt must be greater than 0");

            RuleFor(x => x.Date)
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
                .WithMessage("Repaid date can not be in the past");
        }
    }
}
