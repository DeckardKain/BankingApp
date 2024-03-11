using BankingApp;
using BankingApp.Data;
using BankingApp.Managers;
using BankingApp.Services;
using BankingApp.Services.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddLogging();

builder.Services.AddSingleton<AppManager>();
builder.Services.AddDbContext<BankingAppDbContext>();
builder.Services.AddScoped<IObserver, Observer>();
builder.Services.AddScoped<IDataService, DataService>();
builder.Services.AddScoped<IUIService, UIService>();
builder.Services.AddScoped<IUserInteraction, UserInteraction>();


await builder.Build().RunAsync();
