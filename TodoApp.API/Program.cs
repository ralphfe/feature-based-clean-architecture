using TodoApp.API;
using TodoApp.Application;
using TodoApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var isDevelopment = builder.Environment.IsDevelopment();
{
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddSwagger(isDevelopment);
}

var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.UseSwagger(isDevelopment);
    app.MapEndpoints();
    app.Run();
}

public abstract partial class Program;
