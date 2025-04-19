using System.Xml.Serialization;
using Taxually.TechnicalTest;
using Taxually.TechnicalTest.Handlers;
using Taxually.TechnicalTest.Helpers;
using Taxually.TechnicalTest.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IVatRegistrationHandler, BrittainVatHandler>();
builder.Services.AddScoped<IVatRegistrationHandler, FranceVatHandler>();
builder.Services.AddScoped<IVatRegistrationHandler, GermanyVatHandler>();
builder.Services.AddScoped<TaxuallyQueueClient>();
builder.Services.AddScoped<TaxuallyHttpClient>();
builder.Services.AddScoped<XmlHelper>();

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

app.Run();
