using DSRLearn.Web.Pages.Users.Models;

namespace DSRLearn.Web.Pages.Users.Services
{
    public interface IUserService
    {
        Task<UserModel> GetUser(Guid userId);
        Task<IEnumerable<UserModel>> GetAll();
        Task<UserModel> GetUserByEmail(string email);
        Task<string> GetCurrentEmail();
    }
}
