using System.Net.Http.Json;
using System.Security.Claims;
using DSRLearn.Web.Pages.Users.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace DSRLearn.Web.Pages.Users.Services
{
    public class UserService(HttpClient httpClient) : IUserService
    {
        private readonly AuthenticationStateProvider authenticationStateProvider;
        public UserService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider) : this(httpClient)
        {
            this.authenticationStateProvider = authenticationStateProvider;
        }
        public async Task<UserModel> GetUser(Guid userId)
        {
            var response = await httpClient.GetAsync($"v1/account/{userId}");
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new Exception(content);
            }

            return await response.Content.ReadFromJsonAsync<UserModel>() ?? new();
        }
        public async Task<UserModel> GetUserByEmail(string email)
        {
            var response = await httpClient.GetAsync($"v1/account/GetByEmail/{email}");
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new Exception(content);
            }

            return await response.Content.ReadFromJsonAsync<UserModel>() ?? new();
        }
        public async Task<IEnumerable<UserModel>> GetAll()
        {
            var response = await httpClient.GetAsync($"v1/account");
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new Exception(content);
            }

            return await response.Content.ReadFromJsonAsync<IEnumerable<UserModel>>() ?? new List<UserModel>();
        }
        public async Task<string> GetCurrentEmail()
        {
            var stateprovider = await authenticationStateProvider.GetAuthenticationStateAsync();
            
            var result = stateprovider.User.FindFirst(ClaimTypes.Name)?.Value ?? "";
            
            return result;
        }
    }
}
