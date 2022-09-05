using FreeDictionary.Application.Business;
using FreeDictionary.Application.Interface;
using FreeDictionary.Data.Context;
using FreeDictionary.Data.Interface;
using FreeDictionary.Data.Repository;
using FreeDictionary.Domain;
using FreeDictionary.Service.FreeDictionaryApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

var sqlConnectionString = builder.Configuration.GetValue<string>("ConnectionStrings:Sql");

builder.Services.AddControllers();
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

//builder.Services.AddTransient<IUserRepository, UserRepository>();

//builder.Services.AddTransient<IAuthBusiness, AuthBusiness>();

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
