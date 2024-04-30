namespace DSRLearn.Services.UserAccount;

using System;
using AutoMapper;
using DSRLearn.Common.Exceptions;
using DSRLearn.Common.Validator;
using DSRLearn.Context;
using DSRLearn.Context.Entities;
using DSRLearn.Services.Actions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class UserAccountService : IUserAccountService
{
    private readonly IMapper mapper;
    private readonly UserManager<User> userManager;
    private readonly IModelValidator<RegisterUserAccountModel> registerUserAccountModelValidator;
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IAction action;

    public UserAccountService(
        IMapper mapper, 
        UserManager<User> userManager,
        IAction action,
        IModelValidator<RegisterUserAccountModel> registerUserAccountModelValidator,
        IDbContextFactory<MainDbContext> dbContextFactory
    )
    {
        this.mapper = mapper;
        this.userManager = userManager;
        this.action = action;
        this.registerUserAccountModelValidator = registerUserAccountModelValidator;
        this.dbContextFactory = dbContextFactory;
    }

    public async Task<bool> IsEmpty()
    {
        return !(await userManager.Users.AnyAsync());
    }

    public async Task<Tuple<UserAccountModel, string>> Create(RegisterUserAccountModel model)
    {
        registerUserAccountModelValidator.Check(model);

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
            EmailConfirmed = false,
            PhoneNumber = null,
            PhoneNumberConfirmed = false           
        };

        var result = await userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
            throw new ProcessException($"Creating user account is wrong. {string.Join(", ", result.Errors.Select(s => s.Description))}");

        string code = await userManager.GenerateEmailConfirmationTokenAsync(user);

        return Tuple.Create(mapper.Map<UserAccountModel>(user), code);
    }
    public async Task<string> SendConfirmation(string email, string callbackUrl)
    {
        await action.SendMessage(new SendMeassageModel
        {
            Email = email,
            Subject = "Confirm your account",
            Message = $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>"
        });

        return "Для завершения регистрации проверьте электронную почту и перейдите по ссылке, указанной в письме";
    }
    public async Task ConfirmEmail(string userId, string code)
    {
        if (userId == null || code == null)
        {
            throw new ProcessException($"userId or email is null");
        }
        var user = await userManager.FindByIdAsync(userId);

        if (user == null)
        {
            throw new ProcessException($"no such user with userId");
        }
        var result = await userManager.ConfirmEmailAsync(user, code);
        if (!result.Succeeded)
        {
            throw new ProcessException($"Confirming user account by email is wrong. {string.Join(", ", result.Errors.Select(s => s.Description))}");
        }
    }
    public async Task<UserAccountModel> GetById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        
        var user = await context.Users
            .FirstOrDefaultAsync(x => x.Id == id);

        var result = mapper.Map<UserAccountModel>(user);

        return result;
    }
    public async Task<UserAccountModel> GetByEmail(string email)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        
        var user = await context.Users
            .FirstOrDefaultAsync(x => x.Email == email);

        var result = mapper.Map<UserAccountModel>(user);

        return result;
    }
    public async Task<IEnumerable<UserAccountModel>> GetAll()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        
        var user = await context.Users.ToArrayAsync();

        var result = mapper.Map<IEnumerable<UserAccountModel>>(user);

        return result;
    }
}
