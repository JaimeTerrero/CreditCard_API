using CreditCard.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using CreditCard.Infraestructure.IoC;
using CreditCard.Application.IoC;
using System.Reflection;
using FluentValidation;
using CreditCard.Application.CreditCard.DTOs;
using BankTech.CreditCard.Application.CreditCard.Validators;
using FluentValidation.AspNetCore;
using BankTech.CreditCard.Application.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// For EntityFramework
builder.Services.AddDbContext<CreditCardDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// FluentValidation
FluentValidationConfig.AddFluentValidation(builder.Services);

// Add services to the container
builder.Services
    .AddRepositories()
    .AddService();

// For the AutoMapper Configuration
var mapperAssembly = Assembly.Load("BankTech.CreditCard.Infraestructure");
builder.Services.AddAutoMapper(mapperAssembly);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
