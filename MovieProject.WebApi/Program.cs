using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MovieProject.Application.Interfaces.Redis;
using MovieProject.Application.JWT;
using MovieProject.Application.Registrations;
using MovieProject.Application.Validator.AppUser;
using MovieProject.Persistance.Repositories.Redis;
using MovieProject.WebApi.Registration;
using StackExchange.Redis;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ChangePasswordValidator>());
;


builder.Services.AddScoped<IRedisCacheService, RedisCacheService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApiApplicationServices(builder.Configuration);
builder.Services.ApplicationRegistrationServices(builder.Configuration);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = JwtDefaults.Issuer,
            ValidAudience = JwtDefaults.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtDefaults.Key))
        };
    });


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()  // Tüm domainlerden gelen isteklere izin ver
                  .AllowAnyMethod()  // GET, POST, PUT, DELETE hepsi
                  .AllowAnyHeader(); // Tüm header'lara izin ver
        });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var factory = scope.ServiceProvider.GetRequiredService<IRedisConnectionFactory>();

    if (factory.IsRedisAvailable)
    {
        Console.WriteLine("?? Uygulama Redis ile baþladý");
        Console.WriteLine("?? Cache sistemi aktif");
    }
    else
    {
        Console.WriteLine("?? Uygulama Redis OLMADAN baþladý");
        Console.WriteLine("??  Cache sistemi devre dýþý (fallback mode)");
        Console.WriteLine("?? Arka planda yeniden baðlanma denemeleri yapýlacak");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization(); 
app.MapControllers();
app.Run();
