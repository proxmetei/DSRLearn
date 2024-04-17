using DSRLearn.Api;
using DSRLearn.Api.Configuration;
using DSRLearn.Context;
using DSRLearn.Context.Seeder;
using DSRLearn.Services.Settings;
using DSRLearn.Settings;

var mainSettings = Settings.Load<MainSettings>("Main");
var logSettings = Settings.Load<LogSettings>("Log");
var swaggerSettings = Settings.Load<SwaggerSettings>("Swagger");
var identitySettings = Settings.Load<IdentitySettings>("Identity");

var builder = WebApplication.CreateBuilder(args);

builder.AddAppLogger(mainSettings, logSettings);

var services = builder.Services;

services.AddHttpContextAccessor();

services.AddAppDbContext(builder.Configuration);

services.AddAppCors();

services.AddAppHealthChecks();

services.AddAppVersioning();

services.AddAppSwagger(mainSettings, swaggerSettings, identitySettings);

services.AddAppAutoMappers();

services.AddAppValidator();

services.AddAppAuth(identitySettings);

services.AddAppControllerAndViews();

services.RegisterServices(builder.Configuration);



var app = builder.Build();

app.UseAppCors();

app.UseAppHealthChecks();

app.UseAppSwagger();

app.UseAppAuth();

app.UseAppControllerAndViews();

DbInitializer.Execute(app.Services);

DbSeeder.Execute(app.Services);

app.Run();
