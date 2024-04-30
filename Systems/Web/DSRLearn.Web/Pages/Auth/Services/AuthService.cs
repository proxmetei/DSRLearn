using Blazored.LocalStorage;
using DSRLearn.Web.Pages.Auth.Models;
using DSRLearn.Web.Providers;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json;

namespace DSRLearn.Web.Pages.Auth.Services
{
    public class AuthService: IAuthService
    {
        private const string LocalStorageAuthTokenKey = "authToken";
        private const string LocalStorageRefreshTokenKey = "refreshToken";

        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        public async Task Register(RegisterModel registerModel)
        {
            var requestContent = JsonContent.Create(registerModel);
            
            var response = await _httpClient.PostAsync("v1/account", requestContent);

            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }
        }
        public async Task<LoginResult> Login(LoginModel loginModel)
        {
            var url = $"{Settings.IdentityRoot}/connect/token";

            var request_body = new[]
            {
            new KeyValuePair<string, string>("grant_type", "password"),
            new KeyValuePair<string, string>("client_id", Settings.ClientId),
            new KeyValuePair<string, string>("client_secret", Settings.ClientSecret),
            new KeyValuePair<string, string>("username", loginModel.Email!),
            new KeyValuePair<string, string>("password", loginModel.Password!)
        };

            var requestContent = new FormUrlEncodedContent(request_body);

            var response = await _httpClient.PostAsync(url, requestContent);

            var content = await response.Content.ReadAsStringAsync();

            var loginResult = JsonSerializer.Deserialize<LoginResult>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new LoginResult();
            loginResult.Successful = response.IsSuccessStatusCode;

            if (!response.IsSuccessStatusCode)
            {
                return loginResult;
            }

            await _localStorage.SetItemAsync(LocalStorageAuthTokenKey, loginResult.AccessToken);
            await _localStorage.SetItemAsync(LocalStorageRefreshTokenKey, loginResult.RefreshToken);

            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginModel.Email!);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.AccessToken);

            return loginResult;
        }
        public async Task<AuthenticationHeaderValue?> Refresh()
        {
            var url = $"{Settings.IdentityRoot}/connect/token";
            var r_token = await _localStorage.GetItemAsync<string>(LocalStorageRefreshTokenKey);
            var token = await _localStorage.GetItemAsync<string>(LocalStorageAuthTokenKey);
            var request_body = new[]
            {
            new KeyValuePair<string, string>("grant_type", "refresh_token"),
            new KeyValuePair<string, string>("client_id", Settings.ClientId),
            new KeyValuePair<string, string>("client_secret", Settings.ClientSecret),
            new KeyValuePair<string, string>("token", token),
            new KeyValuePair<string, string>("refresh_token", r_token)
        };

            var requestContent = new FormUrlEncodedContent(request_body);

            var response = await _httpClient.PostAsync(url, requestContent);

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<LoginResult>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new LoginResult();
            result.Successful = response.IsSuccessStatusCode;

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            await _localStorage.SetItemAsync(LocalStorageAuthTokenKey, result.AccessToken);
            await _localStorage.SetItemAsync(LocalStorageRefreshTokenKey, result.RefreshToken);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.AccessToken);
            return new AuthenticationHeaderValue("bearer", result.AccessToken);

        }
        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync(LocalStorageAuthTokenKey);
            await _localStorage.RemoveItemAsync(LocalStorageRefreshTokenKey);

            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();

            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
