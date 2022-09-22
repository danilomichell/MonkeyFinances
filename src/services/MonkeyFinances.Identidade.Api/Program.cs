using MonkeyFinances.Core.Identidade;
using MonkeyFinances.Identidade.Api.Configuration;
using MonkeyFinances.Identidade.Api.Extensions;
using MonkeyFinances.Identidade.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityConfiguration();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddHttpClient("api.financas", c => { c.BaseAddress = new Uri(builder.Configuration.GetSection("ApiFinancas:UrlBase").Value); });
builder.Services.AddScoped<ITokenService, TokenService>();
var appSettingsSection = builder.Configuration.GetSection("ApiFinancas");
builder.Services.Configure<ApiFinancas>(appSettingsSection);
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
