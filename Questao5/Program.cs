using MediatR;
using System.Reflection;
using Questao5.Application.Mapping;
using Questao5.Application.Services;
using Questao5.Infrastructure.Sqlite;
using Questao5.Infrastructure.Database;
using Questao5.Infrastructure.Repositorio;
using Questao5.Application.Services.Interfaces;
using Questao5.Infrastructure.Repositorio.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// sqlite
builder.Services.AddSingleton(new DatabaseConfig { Name = builder.Configuration.GetValue<string>("DatabaseName", "Data Source=database.sqlite") });
builder.Services.AddSingleton<IDatabaseBootstrap, DatabaseBootstrap>();

builder.Services.AddScoped<DbSession>();
builder.Services.AddAutoMapper(typeof(DomainToDtoMappingProfile));
builder.Services.AddScoped<IContaCorrenteRepositorio, ContaCorrenteRepositorio>();
builder.Services.AddScoped<IMovimentoRepositorio, MovimentoRepositorio>();
builder.Services.AddScoped<IMovimentoService,MovimentoService>();
builder.Services.AddScoped<IValidadorContaCorrenteService,ValidadorContaCorrenteService>();
builder.Services.AddScoped<ISaldoContaCorrenteRepositorio, SaldoContaCorrenteRepositorio>();
builder.Services.AddScoped<ISaldoService, SaldoService>();  


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// sqlite
#pragma warning disable CS8602 // Dereference of a possibly null reference.
app.Services.GetService<IDatabaseBootstrap>().Setup();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

app.Run();

// Informações úteis:
// Tipos do Sqlite - https://www.sqlite.org/datatype3.html


