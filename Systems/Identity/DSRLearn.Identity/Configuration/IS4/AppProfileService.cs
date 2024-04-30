using DSRLearn.Context.Entities;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace DSRLearn.Identity.Configuration.IS4
{
    public class AppProfileService : IProfileService
    {
        protected UserManager<User> _userManager;

        public AppProfileService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            //>Processing
            var user = await _userManager.GetUserAsync(context.Subject);

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
        };

            context.IssuedClaims.AddRange(claims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            //>Processing
            var user = await _userManager.GetUserAsync(context.Subject);

            bool confirmed = await _userManager.IsEmailConfirmedAsync(user);

            context.IsActive = (user != null) && confirmed ;
        }
    }
}
