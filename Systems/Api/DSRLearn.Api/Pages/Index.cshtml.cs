using System.Reflection;
using DSRLearn.Common;
using DSRLearn.Services.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSRLearn.Api.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        [BindProperty]
        public bool OpenApiEnabled => settings.Enabled;

        [BindProperty]
        public string IdentityServerUrl => identitySettings.Url;

        [BindProperty]
        public string Version => Assembly.GetExecutingAssembly().GetAssemblyVersion();

        private readonly SwaggerSettings settings;
        private readonly IdentitySettings identitySettings;
        public IndexModel(SwaggerSettings settings, IdentitySettings identitySettings)
        {
            this.settings = settings;
            this.identitySettings = identitySettings;
        }

        public void OnGet()
        {

        }
    }
}
