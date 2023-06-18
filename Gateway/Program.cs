using Gateway.Security;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.IdentityModel.Tokens;

using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;

using System.Security.Cryptography;
using System.Text;

IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("ocelot.json")
                            .Build();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var test = configuration.GetSection("Test").GetSection("Test1").Value;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOcelot(configuration).AddConsul();

var securityOptions = new SecurityOptions();
configuration.Bind(nameof(securityOptions), securityOptions);
builder.Services.AddSingleton(securityOptions);

RsaSecurityKey CleanAndDecodePublicKey(string publicKeyText)
{
    // Remove any unnecessary characters from the public key text
    publicKeyText = publicKeyText
        .Replace("-----BEGIN PUBLIC KEY-----", "")
        .Replace("-----END PUBLIC KEY-----", "")
        .Replace("\n", "")
        .Replace("\r", "");

    try
    {
        // Decode the cleaned public key text
        byte[] publicKeyBytes = Convert.FromBase64String(publicKeyText);
        RSA rsa = RSA.Create();
        rsa.ImportSubjectPublicKeyInfo(publicKeyBytes, out _);
        return new RsaSecurityKey(rsa);
    }
    catch (Exception ex)
    {
        // Handle any exceptions that occur during key decoding
        throw new Exception("Failed to decode public key.", ex);
    }
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer("JWTBearerScheme", opt =>
{
    var publicKeyText = System.IO.File.ReadAllText(securityOptions.PublicKeyFilePath);
    var publicKey = CleanAndDecodePublicKey(publicKeyText);

    opt.SaveToken = true;
    opt.RequireHttpsMetadata = false;
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = publicKey,
        ValidateIssuer = false,
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateLifetime = true
    };
});


CorsPolicyBuilder cbuilder = new CorsPolicyBuilder().AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
//.AllowCredentials().WithOrigins(new string[] { "http://localhost:4200","*" });
CorsPolicy policy = cbuilder.Build();

builder.Services.AddCors(opt => {
    opt.AddPolicy("MyCors", policy);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.UseCors("MyCors");

app.UseOcelot().Wait();


app.MapControllers();

app.Run();
