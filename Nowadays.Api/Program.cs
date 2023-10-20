using Autofac.Extensions.DependencyInjection;
using Autofac;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nowadays.Api.Filters;
using Nowadays.Repository;
using Nowadays.Service.Mapping;
using Nowadays.Service.Validations;
using System.Reflection;
using Nowadays.Api.Modules;
using Nowadays.Api.Middelwares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//FluentValidation - Fiter
builder.Services.AddControllers(options => options.Filters.Add(new ValidateFilterAttribute())).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<CompanyDtoValidator>());

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

//Filter
builder.Services.AddScoped(typeof(NotFoundFilter<,>));

//AutoMapper
builder.Services.AddAutoMapper(typeof(GeneralMapping));

//AutoFac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new RepositoryAndServiceModule());
});

//SqlConnection
builder.Services.AddDbContext<NowadaysContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        option.MigrationsAssembly(assemblyName: Assembly.GetAssembly(typeof(NowadaysContext)).GetName().Name);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseNowadaysException();

app.UseAuthorization();

app.MapControllers();

app.Run();
