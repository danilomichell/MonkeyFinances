using MonkeyFinances.Core.Identidade;
using MonkeyFinances.Financas.Api.Configuration;
using MonkeyFinances.Financas.Api.Configuration.Mediator;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfiguration(builder.Configuration);
builder.Services.AddMediatR();
builder.Services.AddJwtConfiguration(builder.Configuration);

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
