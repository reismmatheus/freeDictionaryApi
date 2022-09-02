using FreeDictionary.Application.Business;
using FreeDictionary.Application.Interface;
using FreeDictionary.Data.Context;
using FreeDictionary.Data.Interface;
using FreeDictionary.Data.Repository;
using FreeDictionary.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

var sqlConnectionString = builder.Configuration.GetValue<string>("ConnectionStrings:Sql");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<FreeDictionaryContext, FreeDictionaryContext>();

builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddTransient<IAuthBusiness, AuthBusiness>();

var connectionString = builder.Configuration.GetConnectionString(@"Data Source=DESKTOP-M9CNHCN\SQLEXPRESS;Initial Catalog=freeDictionaryApi;Integrated Security=True");
// sqlConnectionString


builder.Services.AddDbContext<FreeDictionaryContext>(options => options.UseSqlServer(@"Data Source=DESKTOP-M9CNHCN\SQLEXPRESS;Initial Catalog=freeDictionaryApi;Integrated Security=True"));

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
