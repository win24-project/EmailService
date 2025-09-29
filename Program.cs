using Azure.Identity;
using EmailService.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddAzureKeyVault(
    new Uri("https://group-project-keyvault.vault.azure.net/"),
    new DefaultAzureCredential()
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Email Service API", Version = "v1" });
});

builder.Services.AddScoped<IEmailService, EmailService.Services.EmailService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Email Service API v1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
