using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Portail_OptiVille.Data;
using Portail_OptiVille.Data.Models;
using Portail_OptiVille.Data.Utilities;
using static Portail_OptiVille.Data.Utilities.MailManager;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

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

#region Load Config from Setting.json
// Path to the Setting.json in the wwwroot folder
var configFilePath = Path.Combine(builder.Environment.WebRootPath, "Setting.json");

// Load the config asynchronously and register it as a singleton
var config = await Config.LoadFromJsonAsync(configFilePath);
builder.Services.AddSingleton(config);
#endregion

#region Load Modele from Modele.json
// Path to the Modele.json in the wwwroot folder
var modeleFilePath = Path.Combine(builder.Environment.WebRootPath, "Modele.json");

// Load the modele asynchronously and register it as a singleton
var modeles = await Modele.LoadFromJsonAsync(modeleFilePath);
builder.Services.AddSingleton(modeles);
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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
