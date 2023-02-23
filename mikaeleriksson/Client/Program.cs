using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using mikaeleriksson.Client;
using mikaeleriksson.Client.Services.Interfaces;
using mikaeleriksson.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddSessionStorageServices();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

builder.Services.AddHttpClient("mikaeleriksson.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
 .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
//// Supply HttpClient instances that include access tokens when making requests to the server project
//builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("mikaeleriksson.ServerAPI")); builder.Services.AddHttpClient<PublicClient>(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
//builder.Services.AddMsalAuthentication(options =>
//{
//    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
//    options.ProviderOptions.DefaultAccessTokenScopes.Add(builder.Configuration.GetSection("ServerApi")["Scopes"]);
//});

builder.Services.AddHttpClient<PublicClient>(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
