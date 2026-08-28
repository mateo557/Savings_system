using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema_de_cuenta_de_ahorros.Infrastructure.Context;
using Sistema_de_cuenta_de_ahorros.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. SERVICIOS
//var connectionString = "Server=localhost;Port=3306;Database=cuentabancaria;User Id=root;Password=root;";
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// 2. BUILD
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errores = context.ModelState
            .Where(e => e.Value.Errors.Count > 0)
            .Select(e => new
            {
                //campo = e.Key,
                mensaje = "El valor ingresado debe ser númerico."
            });

        return new BadRequestObjectResult(new
        {
            message = "Datos inválidos el valor ingresado debe ser númerico.",
            //errores
        });
    };
});
builder.Services.AddScoped<ITransactionServices, TransactionServices>();
var app = builder.Build();

// 3. MIDDLEWARES & RUTAS
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReact");
app.UseAuthorization();
app.MapControllers();

// 4. EJECUCIÓN
app.Run();
//app.Run("http://localhost:8081");


