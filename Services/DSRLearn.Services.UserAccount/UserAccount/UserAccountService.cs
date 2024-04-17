namespace DSRLearn.Services.UserAccount;

using AutoMapper;
using DSRLearn.Common.Exceptions;
using DSRLearn.Common.Validator;
using DSRLearn.Context.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class UserAccountService : IUserAccountService
{
    private readonly IMapper mapper;
    private readonly UserManager<User> userManager;
    private readonly IModelValidator<RegisterUserAccountModel> registerUserAccountModelValidator;

    public UserAccountService(
        IMapper mapper, 
        UserManager<User> userManager, 
        IModelValidator<RegisterUserAccountModel> registerUserAccountModelValidator
    )
    {
        this.mapper = mapper;
        this.userManager = userManager;
        this.registerUserAccountModelValidator = registerUserAccountModelValidator;
    }

    public async Task<bool> IsEmpty()
    {
        return !(await userManager.Users.AnyAsync());
    }

    public async Task<UserAccountModel> Create(RegisterUserAccountModel model)
    {
        registerUserAccountModelValidator.Check(model);

        // Create user account
        var user = new User()
        {
            Status = UserStatus.Active,
            Profile = new UserProfile()
            {
                Name = model.Name,
                Surname = model.Surname
            },
            UserName = model.Email,
            Email = model.Email,
            EmailConfirmed = true,
            PhoneNumber = null,
            PhoneNumberConfirmed = false           
        };

        var result = await userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            throw new ProcessException($"Creating user account is wrong. {string.Join(", ", result.Errors.Select(s => s.Description))}");

        return mapper.Map<UserAccountModel>(user);
    }
}
