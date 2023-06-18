using CommonLibrary;
using CommonLibrary.Models.DTOs;

using Consul;

using MediatR;

using PurchaseAPI.Contexts;
using PurchaseAPI.CQRS.Commands;
using PurchaseAPI.CQRS.Handlers;
using PurchaseAPI.CQRS.Queries;
using PurchaseAPI.Infrastructure.Consul;
using PurchaseAPI.Repositories;
using PurchaseAPI.Services;

using System.Net;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PurchaseDBContext>();

builder.Services.AddHttpsRedirection(options =>
{
    options.RedirectStatusCode = (int)HttpStatusCode.TemporaryRedirect;
    options.HttpsPort = 5001;
});
builder.Services.AddAutoMapper(typeof(CommonLibrary.Mapping.MappingConfig));

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<Program>();
    cfg.Lifetime = ServiceLifetime.Scoped;
});

builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(PurchaseRepo<>));
builder.Services.AddScoped<IDbContext, PurchaseDBContext>();

builder.Services.AddScoped<IRequestHandler<AddPurchaseCommand, PurchaseDTO>, AddPurchaseCommandHandler>();
builder.Services.AddScoped<IRequestHandler<GetAllPurchasesQuery, IEnumerable<PurchaseDTO>>, GetAllPurchasesHandler>();

builder.Services.AddScoped<IPurchaseService, PurchaseService>();

var consulAddress = Configuration.GetSection("Consul")["ConsulAddress"];

builder.Services.AddSingleton<IConsulClient, ConsulClient>(provider => new ConsulClient(config => config.Address = new Uri(consulAddress)));
builder.Services.AddSingleton<IConsulRegisterService, ConsulRegisterService>();
builder.Services.AddHostedService<ConsulRegisterService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHsts();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
