using MonkeyFinances.Core.Identidade;
using MonkeyFinances.Identidade.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddGeneralSettings()
    .AddDbContext(builder.Configuration)
    .AddIdentity()
    .AddServiceDependencyInjection()
    .AddJwt(builder.Configuration)
    .AddCustomSwaggerGen()
    .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddControllers()
    .AddCustomJsonOptions();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Total",
        build =>
            build
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
});
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
