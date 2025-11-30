using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WorkTrace.Application;
using WorkTrace.Application.Configurations;
using WorkTrace.Data;
using WorkTrace.Data.Common.Setttings;
using WorkTrace.Logic;
using WorkTrace.Repositories;
using DotNetEnv;

// 1. Cargar variables del archivo .env (si existe en local)
Env.Load();

var builder = WebApplication.CreateBuilder(args);

// --- LECTURA SEGURA DE VARIABLES DE ENTORNO ---
// Esto evita que la app se rompa (crash) si falta una variable en Render.
// Usamos "valores por defecto" seguros donde aplica.

var connectionString = Environment.GetEnvironmentVariable("WORKTRACEDATABASE_CONNECTIONSTRING");
var databaseName = Environment.GetEnvironmentVariable("WORKTRACEDATABASE_DATABASENAME") ?? "WorkTrace";

var jwtSecretKey = Environment.GetEnvironmentVariable("APPLICATIONSETTINGS_SECRETKEY") ?? "Clave_Por_Defecto_Muy_Segura_Para_Evitar_Crash_123";
var jwtIssuer = Environment.GetEnvironmentVariable("APPLICATIONSETTINGS_ISSUER") ?? "WorkTraceApi";
var jwtAudience = Environment.GetEnvironmentVariable("APPLICATIONSETTINGS_AUDIENCE") ?? "WorkTraceClient";
var jwtExpireStr = Environment.GetEnvironmentVariable("APPLICATIONSETTINGS_EXPIREMINUTES");

// Parseo seguro de entero (evita error si el string es nulo o inválido)
int jwtExpireMinutes = int.TryParse(jwtExpireStr, out int m) ? m : 30;

// ----------------------------------------------

// Configuración de Base de Datos
builder.Services.Configure<WorkTraceDatabaseSettings>(options =>
{
    options.ConnectionString = connectionString;
    options.DataBaseName = databaseName;
});

builder.Services.AddControllers();

// Configuración de CORS
// IMPORTANTE: Cambiado a AllowAnyOrigin temporalmente para evitar bloqueos
// cuando despliegues tu Frontend en otra URL (Vercel, Netlify, etc.)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowWebApp", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Inyección de dependencias de tus capas
builder.Services.AddDataServices();
builder.Services.AddRepositoriesServices();
builder.Services.AddLogicServices();
builder.Services.AddApplicationServices();

// Configuración de opciones JWT (Para inyección IOptions)
builder.Services.Configure<JwtSettings>(options =>
{
    options.SecretKey = jwtSecretKey;
    options.Issuer = jwtIssuer;
    options.Audience = jwtAudience;
    options.ExpireMinutes = jwtExpireMinutes;
});

builder.Services.AddEndpointsApiExplorer();

// Configuración de Swagger
builder.Services.AddSwaggerGen(genConfig =>
{
    genConfig.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "WorkTraceApi",
        Description = "Api Empresarial",
        Contact = new OpenApiContact
        {
            Name = "Ricardo",
            Email = "ricardo.vaca@udla.edu.ec",
        }
    });

    genConfig.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter:{your token}"
    });

    genConfig.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
            },
            new string []{}
        }
    });
});

// Configuración de Autenticación JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        // Usamos la variable que leímos de forma segura arriba
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey)),
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
    };
});

builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.

// --- CAMBIO IMPORTANTE PARA RENDER ---
// Hemos sacado Swagger del "if (IsDevelopment)" para que puedas ver la UI
// cuando la app esté desplegada en la nube.
app.UseSwagger();
app.UseSwaggerUI();

// app.UseHttpsRedirection(); // Deshabilitado para evitar problemas con proxies en Docker/Render

app.UseCors("AllowWebApp");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();