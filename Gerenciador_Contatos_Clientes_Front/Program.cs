using Gerenciador_Contatos_Clientes_Front.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);
var backendUrl = Environment.GetEnvironmentVariable("BACKEND_URL");

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

//Injection
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<ContatoService>();

builder.Services.AddScoped(sp =>
{
    var handler = new HttpClientHandler();

    // Optionally: Load a specific certificate from a file or store (uncomment if needed)
    // var cert = new X509Certificate2("path_to_certificate.pfx", "your_certificate_password");
    // handler.ClientCertificates.Add(cert);

    // Optionally: Bypass SSL certificate errors for development (uncomment if needed)
    handler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true;

    // Return an HttpClient using this handler
    return new HttpClient(handler)
    {
        BaseAddress = new Uri(Environment.GetEnvironmentVariable("BACKEND_URL"))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
