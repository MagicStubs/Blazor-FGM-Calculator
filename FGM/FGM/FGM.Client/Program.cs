using FGM.Data.Calculators;
using FGM.Data.ClientServices;
using FGM.Data.Enums;
using FGM.Data.Factory;
using FGM.DataLayer;
using FGM.DataLayer.Calculators;
using FGM.DataLayer.Interfaces;
using FGM.DataLayer.Services;
using MudBlazorWebApp3.Client;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

builder.Services.AddScoped<ISurveyService, ClientSurveyService>();
builder.Services.AddScoped<ICsvDeserializerService<Survey>, SurveyCsvDeserializerService>();
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
        [YongerExtrapolationMethod.FifteenYear] = () =>
            cxt.GetKeyedService<IYongerExtrapolationCalculator>(YongerExtrapolationMethod.FifteenYear)
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
builder.Services.AddScoped(http => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress),
});


await builder.Build().RunAsync();