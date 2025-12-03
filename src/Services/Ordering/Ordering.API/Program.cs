using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

// na Udemy - video 185 postavljanje projekta Ordering

//Add Services to the container

builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);

//-------------------
//Infrastructure - EF Core
//Application - MediatR
//API - Carter, HealthChecks

//builder.Services.
//      .AddApplicationServices()
//      .AddInfrastructureServices(builder.Configuration)
//      .AddWebServices();
//-------------------

var app = builder.Build();
app.UseApiServices();

if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync(); // automatski pokrece EF Core migracije pri startu aplikacije
}

// Configure the HTTP request pipeline.

app.Run();
