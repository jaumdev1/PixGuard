using Domain.Contracts;
using Domain.DTOs;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PixGuard.Api.Application.Contracts;
using PixGuard.Api.Persistence.Repository;
using PixGuard.Api.Persistence;
using PixGuard.Api.Application.Contracts.Mappers;

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using PixGuard.Api.Application;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped(typeof(IRepository<Pix>), typeof(Repository<Pix>));
builder.Services.AddScoped(typeof(PixMapper), typeof(PixMapper));
builder.Services.AddScoped<IAppService<PixDto, CreatePixDto>, PixAppService>();


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

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

