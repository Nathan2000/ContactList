using Blazored.LocalStorage;
using ContactList.Web;
using ContactList.Web.AuthProviders;
using ContactList.Web.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiUrl = builder.Configuration["ApiBaseUrl"];
if (string.IsNullOrEmpty(apiUrl))
{
    throw new ApplicationException("No API URL defined.");
}

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddTransient<JwtAuthorizationMessageHandler>();
builder.Services.AddHttpClient("AuthorizedClient", client =>
{
    client.BaseAddress = new Uri(apiUrl);
})
    .AddHttpMessageHandler<JwtAuthorizationMessageHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("AuthorizedClient"));
builder.Services.AddScoped<ContactService>();

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AppAuthStateProvider>();

await builder.Build().RunAsync();
