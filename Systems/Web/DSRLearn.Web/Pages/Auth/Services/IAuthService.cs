using System.Net.Http.Headers;
using DSRLearn.Web.Pages.Auth.Models;

namespace DSRLearn.Web.Pages.Auth.Services
{
    public interface IAuthService
    {
        Task<LoginResult> Login(LoginModel loginModel);
        Task Logout();
        Task Register(RegisterModel registerModel);
        Task<AuthenticationHeaderValue?> Refresh();
    }
}
