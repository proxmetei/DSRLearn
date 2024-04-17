using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace DSRLearn.Common.ValidationRules
{
    public static class ValidationRuleExtention
    {
        public static IRuleBuilderOptions<T, int> DebtAmount<T>(this IRuleBuilder<T, int> ruleBuilder)
        {
            return ruleBuilder
                .GreaterThan(0).WithMessage("Amount of debt must be greater than 0");
        }
        public static IRuleBuilderOptions<T, DateOnly> DebtRepaidDate<T>(this IRuleBuilder<T, DateOnly> ruleBuilder)
        {
            return ruleBuilder
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
                .WithMessage("Repaid date can not be in the past");
        }
    }
}
