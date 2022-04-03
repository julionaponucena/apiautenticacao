using API;
using API.Routes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterDatabase();
builder.Services.DependencyInjection();

var app = builder.Build();

app.ConfigureRoutes();

app.Run("http://localhost:3001");
