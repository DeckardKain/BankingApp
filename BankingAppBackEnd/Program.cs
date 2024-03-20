using BankingAppBackEnd.Data;
using BankingAppBackEnd.Factories;
using BankingAppBackEnd.Services;
using BankingAppBackEnd.Services.Interfaces;
using BankingAppCore.Models.Customers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BankingAppDbContext>(options =>
{
    options.UseInMemoryDatabase("BankingAppDb");
});

builder.Services.AddHttpClient("BankingAppFrontEnd", client =>
{
    client.BaseAddress = new Uri("https://localhost:7086");
});

// Cors security policy defined here. Is ready for tightening when the time comes.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
    builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

builder.Services.AddScoped(typeof(IDataService<>), typeof(DataService<>));
builder.Services.AddScoped<IDataService<Customer>, DataService<Customer>>();
builder.Services.AddScoped<IDataService<CustomerData>, DataService<CustomerData>>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerDataService, CustomerDataService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<CustomerFactory>();
builder.Services.AddScoped<AccountFactory>();
builder.Services.AddScoped<TransactionFactory>();


var app = builder.Build();

// Seed data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<BankingAppDbContext>();
    dbContext.Database.EnsureCreated();
    await dbContext.SeedInitialData(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
