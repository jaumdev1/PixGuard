using System.Text;
using Domain.Contracts;
using Domain.DTOs;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PixGuard.Api.Application.Contracts;
using PixGuard.Api.Persistence.Repository;
using PixGuard.Api.Persistence;
using PixGuard.Api.Application.Contracts.Mappers;

using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PixGuard.Api.Application;
using PixGuard.Api.Persistence.Middlewares;
using PixGuard.Api.Presentation.CustomExceptionHandler;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));



var SecretKey = builder.Configuration.GetSection("AppSettings")["SecretKey"];
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        IssuerSigningKey = key,
        ValidateIssuer = false
    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            context.Token = context.Request.Cookies["Token"];
            return Task.CompletedTask;
        }
    };
});




builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IPixRepository, PixRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped(typeof(PixMapper), typeof(PixMapper));
builder.Services.AddScoped(typeof(UserMapper), typeof(UserMapper));
builder.Services.AddScoped(typeof(AssessmentMapper), typeof(AssessmentMapper));
builder.Services.AddScoped<IPixAppService, PixAppService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IAppService<UserDto, CreateUserDto>, UserAppService>();
builder.Services.AddScoped<IAppService<AssessmentDto, CreateAssessmentDto>, AssessmentAppService>();





var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.Run();

