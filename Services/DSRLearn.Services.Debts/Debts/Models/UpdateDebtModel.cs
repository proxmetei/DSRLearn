using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DSRLearn.Common.ValidationRules;
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
            RuleFor(x => x.Amount).DebtAmount();

            RuleFor(x => x.RepaidDate).DebtRepaidDate();
        }
    }
}
