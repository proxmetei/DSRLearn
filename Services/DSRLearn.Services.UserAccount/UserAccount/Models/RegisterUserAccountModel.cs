namespace DSRLearn.Services.UserAccount;

using AutoMapper;
using DSRLearn.Context;
using DSRLearn.Context.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

public class RegisterUserAccountModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
public class RegisterUserAccountModelProfile : Profile
{
    public RegisterUserAccountModelProfile()
    {
        CreateMap<RegisterUserAccountModel, User>()
            .ForPath(d => d.Profile.Name, o => o.MapFrom(s => s.Name))
            .ForPath(d => d.Profile.Surname, o => o.MapFrom(s => s.Surname))
            ;
    }
}
public class RegisterUserAccountModelValidator : AbstractValidator<RegisterUserAccountModel>
{
    public RegisterUserAccountModelValidator(IDbContextFactory<MainDbContext> contextFactory)
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("User name is required.");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Email is required.")
            .Must((email) =>
            {
                using var context = contextFactory.CreateDbContext();
                var found = context.Users.Any(a => a.Email == email);
                return !found;
            }).WithMessage("This email is already being used");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MaximumLength(50).WithMessage("Password is long.");
    }
}