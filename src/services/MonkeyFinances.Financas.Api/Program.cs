using MediatR;
using Microsoft.AspNetCore.Hosting;
using MonkeyFinances.Core.Identidade;
using MonkeyFinances.Financas.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);
var assembly = AppDomain.CurrentDomain.Load("MonkeyFinances.Financas.Api");
builder.Services.AddApiConfiguration(builder.Configuration);
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddMediatR(assembly);

builder.Services.RegisterServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("Total");
app.UseAuthConfiguration();

app.MapControllers();

app.Run();
