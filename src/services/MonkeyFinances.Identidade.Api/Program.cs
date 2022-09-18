using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MonkeyFinances.Identidade.Api.Data;
using MonkeyFinances.Identidade.Api.Entensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentidadeConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddErrorDescriber<IdentityPortugueseErrors>();

var jwtSettingsSection = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSettingsSection);
var jwtSettings = jwtSettingsSection.Get<JwtSettings>();
var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(bearerOptions =>
{
    bearerOptions.RequireHttpsMetadata = true;
    bearerOptions.SaveToken = true;
    bearerOptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidAudience = jwtSettings.ValidAt,
        ValidIssuer = jwtSettings.Issuer
    };
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
