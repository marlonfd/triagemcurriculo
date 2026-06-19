using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Internal;
using TriagemCurriculos.Infraestructure;
using TriagemCurriculos.Infraestructure.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var connString = builder.Configuration.GetConnectionString("connstr-mysql");

builder.Services.AddDbContext<MainDbContext>(options =>
options.UseMySql(connString, ServerVersion.AutoDetect(connString),
mysqlOptions => {
    mysqlOptions.CommandTimeout(60);
    })
);

builder.Services.RegisterServices(builder);
builder.Services.RegisterRepositories();
builder.Services.AddScoped<GlobalMiddleware>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseMiddleware<GlobalMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health").AllowAnonymous();

app.Run();
