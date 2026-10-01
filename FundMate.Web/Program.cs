using FundMate.Application.AppSettingsBindings;
using FundMate.Application.Interfaces;
using FundMate.Application.Mappings;
using FundMate.Application.Services;
using FundMate.Data;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Repository.Hierarchy;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDataContext>(
    opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt => 
        opt.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            )
        }
    );

builder.Services.Configure<RouteOptions>(opt => opt.LowercaseUrls = true);
builder.Services.AddControllers();

// Add services
builder.Services.AddSingleton<IPasswordHasherService, PasswordHasherService>();
builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Mapster config
var config = TypeAdapterConfig.GlobalSettings;
config.Scan(typeof(UserMapping).Assembly); // Scans current assembly for IRegister
builder.Services.AddSingleton(config);
builder.Services.AddScoped<IMapper, ServiceMapper>();

// JWT Config
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

// log4Net Config
// Production has no Console appender (see log4net.Production.config); every
// other environment (Development, Staging, etc.) uses log4net.Development.config.
var log4NetConfigFile = builder.Environment.IsProduction()
    ? "log4net.Production.config"
    : "log4net.Development.config";
builder.Logging.AddLog4Net(log4NetConfigFile);

var repository = LogManager.GetRepository(Assembly.GetEntryAssembly()!);
var hierarchy = repository as Hierarchy;
var logConnectionString = builder.Configuration["LogDbConnection"] ?? builder.Configuration.GetConnectionString("DefaultConnection");

// Dedicated log DB configured (e.g. Docker/production) — write logs via SQL.
using (var connection = new Npgsql.NpgsqlConnection(logConnectionString))
{
    connection.Open();
    using var command = connection.CreateCommand();
    command.CommandText = @"
        CREATE TABLE IF NOT EXISTS ""Logs"" (
            ""Id"" SERIAL PRIMARY KEY,
            ""Timestamp"" timestamptz NOT NULL,
            ""ApplicationName"" varchar(100) NOT NULL,
            ""Level"" varchar(20) NOT NULL,
            ""Logger"" varchar(255) NOT NULL,
            ""Message"" text NOT NULL,
            ""Exception"" text NULL
        );";
    command.ExecuteNonQuery();
}

var adoAppender = hierarchy!.Root.Appenders.OfType<AdoNetAppender>().FirstOrDefault();
if (adoAppender is not null)
{
    adoAppender.ConnectionString = logConnectionString;
    adoAppender.ActivateOptions();
}

GlobalContext.Properties["ApplicationName"] = builder.Configuration["AppName"] ?? "FundMate";

var startupLogger = LogManager.GetLogger(typeof(Program));
startupLogger.Info("FundMate application has started.");

// Swagger UI Configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();