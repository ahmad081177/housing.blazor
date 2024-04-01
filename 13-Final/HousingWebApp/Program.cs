using HousingWebApp.Models;
using HousingWebApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddDbContext<HousingDBContext>(
    options=>options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
        .Replace("[DataDirectory]",Directory.GetCurrentDirectory())
        )
    );

//Scoped is good for services that are used by the same client
//For insatance, save cookies, session (state), etc
//builder.Services.AddScoped<ZZZ>();

builder.Services.AddSingleton<EmailService>(); //On instance, at server level
builder.Services.AddSingleton<AddressService>();
builder.Services.AddSingleton<ImageService>(provider =>
{
    var storageConfiguration = builder.Configuration.GetSection("Storage");
    var imagesFolderPath = storageConfiguration["Images"];
    return new ImageService(imagesFolderPath);
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
