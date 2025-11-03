var builder = WebApplication.CreateBuilder(args);

// na Udemy - video 185 postavljanje projekta Ordering

//Add Services to the container

//DependencyInjection.AddApplicationServices(builder.Services);

//builder.Services
//    .AddInfrastructureServices(builder.Configuration)
//    .AddApiServices();

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

// Configure the HTTP request pipeline.

app.Run();
