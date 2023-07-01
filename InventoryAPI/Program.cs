using CommonLibrary;
using CommonLibrary.Models;
using CommonLibrary.Repositories;

using InventoryAPI.Contexts;
using InventoryAPI.CQRS.Commands;
using InventoryAPI.CQRS.Handlers;
using InventoryAPI.CQRS.Queries;
using InventoryAPI.Services;

using MediatR;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<InventoryDBContext>(p => p.UseSqlServer(Configuration.GetConnectionString("InventoryConn")));
builder.Services.AddScoped(typeof(IGenericRepo<,>), typeof(GenericRepo<,>));
builder.Services.AddScoped<IInventoryService, InventoryService>();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<Program>();
    cfg.Lifetime = ServiceLifetime.Scoped;
});

builder.Services.AddScoped<IRequestHandler<AddInventoryCommand, Inventory>, AddInventoryCommandHandler>();
builder.Services.AddScoped<IRequestHandler<GetAllInventoryQuery, IEnumerable<Inventory>>, GetAllInventoryQueryHandler>();

builder.Services.AddHostedService<InventoryBackGroundService>();

var app = builder.Build();

// Configure the HTTP request pipeline.s
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
