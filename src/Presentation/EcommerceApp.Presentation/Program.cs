using Autofac;
using Autofac.Extensions.DependencyInjection;
using Ecommerce.Infrastructure.Context;
using EcommerceApp.Application.IoC;
using EcommerceApp.Presentation.Models.SeedDataModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ECommerceAppDbContext>(_ =>
{
    _.UseSqlServer(builder.Configuration.GetConnectionString("EcommerceConnString"));
});
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new DependencyResolver());
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(_ =>
{
    _.LoginPath = "/Login/Login";
    _.Cookie = new CookieBuilder
    {
        Name="EcommerceCookie",
        SecurePolicy=CookieSecurePolicy.Always,
        HttpOnly=true
    };
    _.ExpireTimeSpan = TimeSpan.FromMinutes(10);
    _.SlidingExpiration = false;  //when request come, cookie time will be same
    _.Cookie.MaxAge = _.ExpireTimeSpan;
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    //You can set Time   
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
SeedData.Seed(app);
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
