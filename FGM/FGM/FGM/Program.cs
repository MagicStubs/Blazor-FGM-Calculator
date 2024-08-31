using FGM.Components;
using FGM.Components.Account;
using FGM.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FGM.Client.Pages;
using FGM.Client.Shared;
using FGM.Client.Shared.Surveys;
using FGM.Data.Calculators;
using FGM.Data.Enums;
using FGM.Data.Factory;
using FGM.DataLayer;
using FGM.DataLayer.Calculators;
using FGM.DataLayer.Interfaces;
using FGM.DataLayer.Services;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddMudServices();

builder.Services.AddControllers();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

RegisisterDependecies();



builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<FgmDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<FgmDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();


builder.Services.AddScoped(http => new HttpClient
{
    BaseAddress = new Uri(builder.Configuration.GetSection("BaseUri").Value!),
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapControllers();

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(SurveyData).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();

return;


void RegisisterDependecies()
{
    builder.Services.AddScoped<ICsvDeserializerService<Survey>, SurveyCsvDeserializerService>();
    builder.Services.AddScoped<ISurveyService, SurveyService>();
    builder.Services.AddScoped<IFgmCalculator, FgmCalculator>();
    builder.Services.AddScoped<IExtrapolationService, ExtrapolationService>();
    builder.Services.AddScoped<ICsvExporter<CalculatedSurvey>, SurveyCsvExporter>();
    builder.Services.AddKeyedScoped<IYongerExtrapolationCalculator, FiveYearExtrapolationCalculator>(YongerExtrapolationMethod
        .FiveYear);
    builder.Services.AddKeyedScoped<IYongerExtrapolationCalculator, TenYearExtrapolationCalculator>(YongerExtrapolationMethod
        .TenYear);
    builder.Services.AddKeyedScoped<IYongerExtrapolationCalculator, FiveYearExtrapolationCalculator>(YongerExtrapolationMethod
        .FifteenYear);
    builder.Services.AddScoped<IYongerExtrapolationCalculatorFactorty>(cxt =>
    {
        var calculators = new Dictionary<YongerExtrapolationMethod, Func<IYongerExtrapolationCalculator>>()
        {
            [YongerExtrapolationMethod.FiveYear] =
                () => cxt.GetKeyedService<IYongerExtrapolationCalculator>(YongerExtrapolationMethod.FiveYear),
            [YongerExtrapolationMethod.TenYear] =
                () => cxt.GetKeyedService<IYongerExtrapolationCalculator>(YongerExtrapolationMethod.TenYear),
            [YongerExtrapolationMethod.FifteenYear] = 
                () => cxt.GetKeyedService<IYongerExtrapolationCalculator>(YongerExtrapolationMethod.FifteenYear)
        };
        return new YongerExtrapolationCalculatorFactory(calculators);
    });

    builder.Services.AddKeyedScoped<IOlderExtrapolationCalculator, FlattenExtrapolationCalculator>(
        OlderExtrapolationMethod.Flatten);
    builder.Services.AddScoped<IOlderExtrapolationCalculatorFactorty>(cxt =>
    {
        var calculators = new Dictionary<OlderExtrapolationMethod, Func<IOlderExtrapolationCalculator>>()
        {
            [OlderExtrapolationMethod.Flatten] =
                () => cxt.GetKeyedService<IOlderExtrapolationCalculator>(OlderExtrapolationMethod.Flatten),
          
        };
        return new OlderExtrapolationCalculatorFactory(calculators);
    });
    builder.Services.AddScoped<IMeanSurveyCalculator, MeanSurveyCalculator>();

    builder.Services.AddScoped<FgmDbContext>();
}