using AutoMapper;
using DSRLearn.Context;
using DSRLearn.Context.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DSRLearn.Services.Debts
{
    public class CreateDebtModel
    {
        public Guid DebtorId { get; set; }
        public Guid CreditorId { get; set; }
        public int Amount { get; set; }
        public DateOnly RepaidDate { get; set; }
    }
    public class CreateDebtModelProfile : Profile
    {
        public CreateDebtModelProfile()
        {
            CreateMap<CreateDebtModel, Debt>()
                .ForMember(dest => dest.CreditorId, opt => opt.Ignore())
                .ForMember(dest => dest.DebtorId, opt => opt.Ignore())
                .AfterMap<CreateDebtModelActions>();
        }

        public class CreateDebtModelActions : IMappingAction<CreateDebtModel, Debt>
        {
            private readonly IDbContextFactory<MainDbContext> contextFactory;

            public CreateDebtModelActions(IDbContextFactory<MainDbContext> contextFactory)
            {
                this.contextFactory = contextFactory;
            }

            public void Process(CreateDebtModel source, Debt destination, ResolutionContext context)
            {
                using var db = contextFactory.CreateDbContext();

                var creditor = db.Users.FirstOrDefault(x => x.Uid == source.CreditorId);
                var debtor = db.Users.FirstOrDefault(x => x.Uid == source.DebtorId);

                destination.CreditorId = creditor.Id;
                destination.DebtorId = debtor.Id;
            }
        }
    }

    public class CreateBookModelValidator : AbstractValidator<CreateDebtModel>
    {
        public CreateBookModelValidator(IDbContextFactory<MainDbContext> contextFactory)
        {
            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount of debt must be greater than 0");

            RuleFor(x => x.DebtorId)
                .NotEmpty().WithMessage("Debtor is required")
                .Must((id) =>
                {
                    using var context = contextFactory.CreateDbContext();
                    var found = context.Users.Any(a => a.Uid == id);
                    return found;
                }).WithMessage("Debtor not found");

            RuleFor(x => x.CreditorId)
                .NotEmpty().WithMessage("Creditor is required")
                .Must((id) =>
                {
                    using var context = contextFactory.CreateDbContext();
                    var found = context.Users.Any(a => a.Uid == id);
                    return found;
                }).WithMessage("Creditor not found");

            RuleFor(x => x.RepaidDate)
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
                .WithMessage("Repaid date can not be in the past");
        }
    }
}
