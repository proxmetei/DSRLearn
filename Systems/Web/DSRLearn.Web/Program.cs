using System.Net.Http;
using Blazored.LocalStorage;
using DSRLearn.Web;
using DSRLearn.Web.Handler;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddAuthorizationCore();

builder.Services.AddMudServices();

builder.Services.AddBlazoredLocalStorage();

builder.Services.RegisterServices(builder.Configuration);

builder.Services.AddScoped(sp =>
new HttpClient(sp.GetRequiredService<DelegatingHandler>())
{ BaseAddress = new Uri(Settings.ApiRoot) });

await builder.Build().RunAsync();
