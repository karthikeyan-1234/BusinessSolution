using Authenticate.Contexts;
using Authenticate.Infrastructure.Consul;
using Authenticate.Models;
using Authenticate.Security;
using Authenticate.ServiceInjection;
using Authenticate.Services;

using Consul;

using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var securityOptions = new SecurityOptions();
Configuration.Bind(nameof(securityOptions), securityOptions);
builder.Services.AddSingleton(securityOptions);

//builder.Services.AddRsaKeys(securityOptions);

builder.Services.AddDbContext<ApplicationDBContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("IdentityConStr")));
builder.Services.AddDbContext<TokenDBContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("TokenConStr")));


builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDBContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ITenantService, TenantService>();

CorsPolicyBuilder cbuilder = new CorsPolicyBuilder().AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200").AllowCredentials();
CorsPolicy policy = cbuilder.Build();

builder.Services.AddCors(opt => {
    opt.AddPolicy("MyCors", policy);
});


var consulAddress = Configuration.GetSection("Consul")["ConsulAddress"];

builder.Services.AddSingleton<IConsulClient, ConsulClient>(provider => new ConsulClient(config => config.Address = new Uri(consulAddress)));
builder.Services.AddSingleton<IConsulRegisterService, ConsulRegisterService>();
builder.Services.AddHostedService<ConsulRegisterService>();


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

app.UseCors("MyCors");

app.Run();
