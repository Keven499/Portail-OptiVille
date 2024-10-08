using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Portail_OptiVille.Data;
using Portail_OptiVille.Data.Models;
using Portail_OptiVille.Data.Utilities;
using static Portail_OptiVille.Data.Utilities.MailManager;
using System.Net.Http;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<NEQManager>();

#region Load Config from Setting.json
var configFilePath = Path.Combine(builder.Environment.WebRootPath, "Setting.json");
var config = await Config.LoadFromJsonAsync(configFilePath);
builder.Services.AddSingleton(config);
#endregion

#region Load Modele from Modele.json
var modeleFilePath = Path.Combine(builder.Environment.WebRootPath, "Modele.json");
var modeles = await Modele.LoadFromJsonAsync(modeleFilePath);
builder.Services.AddSingleton(modeles);
#endregion

#region Mail
//Envoie de mail
builder.Services.Configure<DefaultMail>(builder.Configuration.GetSection("DefaultMail"));
builder.Services.AddScoped<MailManager>();
#endregion

#region Database
//Injection de d�pendance pour la connexion � la base de donn�es
builder.Services.AddDbContext<A2024420517riGr1Eq6Context>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 18)) // Remplacez par une version g�n�rique
    )
);
#endregion
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<DownloadService>();

#region Connexion
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication("Cookies")
    .AddCookie(options =>
    {
        options.LoginPath = "/Connexion-employe";  // Page de connexion
    });
builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddAuthorizationCore();
#endregion

//builder.Services.AddHttpClient();

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

// Ajout de l'authentification et de l'autorisation
app.UseAuthentication();  // Middleware d'authentification
app.UseAuthorization();   // Middleware d'autorisation

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
