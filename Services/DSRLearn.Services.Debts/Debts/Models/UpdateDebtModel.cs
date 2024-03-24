using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DSRLearn.Context.Entities;
using FluentValidation;

namespace DSRLearn.Services.Debts
{
    public class UpdateDebtModel
    {
        public int Amount { get; set; }
        public DateOnly RepaidDate { get; set; }
    }

    public class UpdateDebtModelProfile : Profile
    {
        public UpdateDebtModelProfile()
        {
            CreateMap<UpdateDebtModel, Debt>();
        }
    }

    public class UpdateModelValidator : AbstractValidator<UpdateDebtModel>
    {
        public UpdateModelValidator()
        {
            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount of debt must be greater than 0");

            RuleFor(x => x.RepaidDate)
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
                .WithMessage("Repaid date can not be in the past");
        }
    }
}
