using System.Security.Claims;

namespace DSRLearn.Services.UserAccount;

public interface IUserAccountService
{
    Task<bool> IsEmpty();

    /// <summary>
    /// Create user account
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<Tuple<UserAccountModel, string>> Create(RegisterUserAccountModel model);
    Task<UserAccountModel> GetByEmail(string email);
    Task<UserAccountModel> GetById(Guid id);
    Task<IEnumerable<UserAccountModel>> GetAll();
    Task<string> SendConfirmation(string callbackUrl, string email);
    Task ConfirmEmail(string userId, string code);


    // .. Также здесь можно разместить методы для изменения данных учетной записи, восстановления и смены пароля, подтверждения электронной почты, установки телефона и его подтверждения и т.д.
    // .. Но это уже на самостоятельно.
    // .. Удачи! Я в вас верю!  :)
}
