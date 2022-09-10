using FreeDictionary.API.Exceptions;
using FreeDictionary.Application.Business;
using FreeDictionary.Application.Interface;
using FreeDictionary.Application.Model;
using FreeDictionary.Data.Context;
using FreeDictionary.Data.Interface;
using FreeDictionary.Data.Repository;
using FreeDictionary.Domain;
using FreeDictionary.Service.FreeDictionaryApi;
using FreeDictionary.Service.RedisCache;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Data.Entity;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var sqlConnectionString = builder.Configuration.GetValue<string>("ConnectionStrings:Sql");
var secretKey = builder.Configuration.GetValue<string>("AppSettingsConfiguration:SecretKey");

builder.Services.Configure<AppSettingsConfiguration>(builder.Configuration.GetSection("AppSettingsConfiguration"));

builder.Services.AddStackExchangeRedisCache(options => { options.Configuration = "localhost:6379"; });
builder.Services.AddTransient<IRedisCacheClient, RedisCacheClient>();

builder.Services.AddControllers();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<FreeDictionaryContext, FreeDictionaryContext>();

#region Repositories

typeof(UserRepository)
        .Assembly
        .GetTypes()
        .Select(x => x.GetInterfaces().Where(y => y.Namespace == "FreeDictionary.Data.Interface"))
        .Where(x => x.Any()).ToList()
        .ForEach(assignedTypes =>
        {
            var interfaceType = assignedTypes.FirstOrDefault(f => f.Name != "IRepository`1");
            if (interfaceType != null)
            {
                var classType = typeof(UserRepository).Assembly.GetTypes()
                    .Where(w => w.Namespace == "FreeDictionary.Data.Repository")
                    .First(f => f.Name == $"{interfaceType.Name.Substring(1)}");

                if (builder.Services.Any(a => a.ServiceType.Name != interfaceType.Name))
                    builder.Services.AddTransient(interfaceType, classType);
            }
        });

#endregion

#region Business

typeof(UserBusiness)
    .Assembly
    .GetTypes()
    .Select(x => x.GetInterfaces().Where(y => y.Namespace == "FreeDictionary.Application.Interface"))
    .Where(x => x.Any()).ToList()
    .ForEach(assignedTypes =>
    {
        var interfaceType = assignedTypes.First();
        var classType = typeof(UserBusiness).Assembly.GetTypes()
            .Where(w => w.Namespace == "FreeDictionary.Application.Business")
            .First(f => f.Name == $"{interfaceType.Name.Substring(1)}");

        builder.Services.AddTransient(interfaceType, classType);
    });

#endregion

#region Clients - CrossCut

builder.Services.AddTransient<IFreeDictionaryApiClient, FreeDictionaryApiClient>();

builder.Services.AddHttpClient();

#endregion

var key = Encoding.ASCII.GetBytes(secretKey);
builder.Services
    .AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
     .AddJwtBearer(x =>
     {
         x.RequireHttpsMetadata = false;
         x.SaveToken = true;
         x.TokenValidationParameters = new TokenValidationParameters
         {
             ValidateIssuerSigningKey = true,
             IssuerSigningKey = new SymmetricSecurityKey(key),
             ValidateIssuer = false,
             ValidateAudience = false
         };
     });

builder.Services.AddDbContext<FreeDictionaryContext>(options => options.UseSqlServer(sqlConnectionString));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }