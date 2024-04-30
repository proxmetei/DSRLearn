namespace DSRLearn.Identity.Configuration;

using DSRLearn.Common.Security;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;

public static class AppClients
{
    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "client",
                ClientSecrets =
                {
                    new Secret("A3F0811F2E934C4F1114CB693F7D785E".ToSha256())
                },

                AllowedGrantTypes = GrantTypes.ClientCredentials,

                AccessTokenLifetime = 3600, // 1 hour

                AllowedScopes = {
                    AppScopes.Admin,
                    AppScopes.User
                }
            }
            ,
            new Client
            {
                ClientId = "frontend",
                ClientSecrets =
                {
                    new Secret("A3F0811F2E934C4F1114CB693F7D785E".ToSha256())
                },

                AllowedGrantTypes = {"password", "refresh_token"},
                AllowOfflineAccess = true,
                AccessTokenType = AccessTokenType.Jwt,
                AccessTokenLifetime = 3600, // 1 hour
                AlwaysSendClientClaims = true,
                RefreshTokenUsage = TokenUsage.OneTimeOnly,
                IdentityTokenLifetime = 3600,
                RefreshTokenExpiration = TokenExpiration.Sliding,
                AbsoluteRefreshTokenLifetime = 2592000, // 30 days
                SlidingRefreshTokenLifetime = 1296000, // 15 days
                AllowedScopes = {
                    AppScopes.User,
                    AppScopes.Admin,
                    IdentityServerConstants.StandardScopes.Profile
                }
            }
        };
}