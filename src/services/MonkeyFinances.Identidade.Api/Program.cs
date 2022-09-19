using MonkeyFinances.Core.Identidade;
using MonkeyFinances.Identidade.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityConfiguration();

builder.Services.AddApiConfiguration();

builder.Services.AddSwaggerConfiguration();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

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
