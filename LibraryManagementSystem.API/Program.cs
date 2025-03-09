using LibraryManagementSystem.Core.Interfaces;
using LibraryManagementSystem.Infrastructure;
using System.Reflection;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.InfrastructureConfiguration(builder.Configuration);

// Configure Automapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

var domainEventPublisher = app.Services.GetRequiredService<IDomainEventPublisher>();
var bookAddedObserver = app.Services.GetRequiredService<IBookAddedObserver>();
domainEventPublisher.Subscribe(bookAddedObserver);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
