using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Internal;
using TriagemCurriculos.Infraestructure;
using TriagemCurriculos.Infraestructure.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});

builder.Services.AddAuthorization();

var connString = builder.Configuration.GetConnectionString("connstr-mysql");

builder.Services.AddDbContext<MainDbContext>(options =>
options.UseMySql(connString, ServerVersion.AutoDetect(connString),
mysqlOptions => {
    mysqlOptions.CommandTimeout(60);
    })
);
builder.Services.AddHealthChecks();
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.MapHealthChecks("/health").AllowAnonymous();

app.Run();
