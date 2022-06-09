#region - Imports
using System.Reflection;
using COE000.Portal.NomeProjeto.Data;
using COE000.Portal.NomeProjeto.Util;
using Microsoft.EntityFrameworkCore;
using COE000.Portal.NomeProjeto.Models;
using COE000.Portal.NomeProjeto.Reposity;
using COE000.Portal.NomeProjeto.Reposity.Entity;
using COE000.Portal.NomeProjeto.Areas.Identity.Data;
#endregion

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
    .AddJsonFile("appsettings.json");

#if DEBUG
var connectionString = builder.Configuration.GetConnectionString("HomologConnection");
#else
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
#endif

builder.Services.AddDbContext<DataBaseContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<IdentityBuilderContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IncriseUserModel>(options => 
    options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<IdentityBuilderContext>();

builder.Services.AddTransient<EmailService>();
builder.Services.AddTransient<NotifyModel>();
builder.Services.AddTransient<DataBaseContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();