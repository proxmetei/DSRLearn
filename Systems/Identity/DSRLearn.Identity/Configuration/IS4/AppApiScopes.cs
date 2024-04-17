namespace DSRLearn.Identity.Configuration;

using DSRLearn.Common.Security;
using Duende.IdentityServer.Models;

public static class AppApiScopes
{
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope(AppScopes.User, "user"),
            new ApiScope(AppScopes.Admin, "admin")
        };
}