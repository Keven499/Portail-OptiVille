using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Portail_OptiVille.Data;
using Portail_OptiVille.Data.Models;
using Portail_OptiVille.Data.Utilities;
using static Portail_OptiVille.Data.Utilities.MailManager;

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
