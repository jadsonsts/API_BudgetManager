using BudgetManager.Api;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Class e Interfaces IOC
Func<IServiceProvider, MySqlConnection> Connect = a => new MySqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddTransient(Connect);
builder.Services.AddScoped(typeof(IConnection), typeof(Connection));
builder.Services.AddScoped(typeof(IDalCustomer), typeof(DalCustomer));
builder.Services.AddScoped(typeof(IDalCategories), typeof(DalCategories));
builder.Services.AddScoped(typeof(IDalTransaction), typeof(DalTransaction));
builder.Services.AddScoped(typeof(IDalWallet), typeof(DalWallet));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
