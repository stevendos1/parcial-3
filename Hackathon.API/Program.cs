using Microsoft.EntityFrameworkCore;
using Hackathon.Shared.Entities;
using Hackathon.API.Data;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuraci�n de servicios
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar controladores
builder.Services.AddControllers();

// Configuraci�n de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hackathon API", Version = "v1" });
});

// Configuraci�n de pol�ticas de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Aplicar pol�ticas de CORS
app.UseCors("AllowAll");

// Configurar la tuber�a de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hackathon API v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Mapear los controladores
app.MapControllers();

app.Run();
