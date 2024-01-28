using Framework.Security2023;
using NLog;
using NLog.Web;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Application.Services;
using Application.IServices;
using Framework.Security2023.Services;
using Framework.Security2023.IServices;
using Domain.IRepositories;
using Domain.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().
    GetCurrentClassLogger();
logger.Debug("init main");
try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.RequireAuthenticatedSignIn = true;
        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
       
    }).AddCookie(options =>
    {
        options.Cookie.Name = "ACookie$";
        options.ExpireTimeSpan = TimeSpan.FromHours(2);
        options.Events = new CookieAuthenticationEvents() { 
            OnRedirectToLogin = x =>
            {
                x.Response.Redirect("Login");
                return Task.CompletedTask;
            },
            OnRedirectToAccessDenied = x =>
            {
                x.Response.Redirect("AccesDenied");
                return Task.CompletedTask;
            }
        };
    });

    string connectionStr = builder.Configuration.GetConnectionString("SqlServer");
    string connectionStrFkw = builder.Configuration.GetConnectionString("SqlServerFkw");
    string languaje = builder.Configuration.GetSection("DefaultLanguaje").Value;
    SlqConnectionStr.Instance.SqlConnectionString = connectionStrFkw;

    // Add services to the container.
    builder.Services.AddControllersWithViews();  
    //CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(languaje);
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();   
    builder.Services.AddResponseCaching();
    builder.Services.AddTransient<IServiceLogin, ServiceLogin>();
    builder.Services.AddTransient<IServiceJob, ServiceJob>();
    builder.Services.AddTransient<IServiceUser, ServiceUser>();
    builder.Services.AddTransient<IServiceCatalog<Area>, ServiceCatalogArea>();
    builder.Services.AddTransient<IServiceCatalog<Company>, ServiceCatalogCompany>();
    builder.Services.AddTransient<IRepositoryJob, RepositoryJob>();
    //builder.Services.AddSession(options =>
    //{
    //    options.IdleTimeout = TimeSpan.FromMinutes(60);
    //});
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddDbContext<CatalogContext>(options => options.UseSqlServer(connectionStr));
    builder.Services.AddDbContextFactory<JobContext>(options => options.UseSqlServer(connectionStr));
    builder.Services.AddSession();
    builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
    builder.Services.AddMvc().AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();

    var app = builder.Build();
    app.UseResponseCaching();
    //app.Use(async(context, next) =>
    //{
    //    context.Response.GetTypedHeaders().CacheControl = new Microsoft.Net.Http.Headers.CacheControlHeaderValue { MaxAge = TimeSpan.FromSeconds(10), Public = true };
    //    await next();
    //});

    app.UseCookiePolicy();
    app.UseSession();
    var supportedCultures = new[]
    {
        "es"
    };
    var options = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
        .AddSupportedCultures(supportedCultures).AddSupportedUICultures(supportedCultures);
    app.UseRequestLocalization(options);
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
    app.UseAuthorization();
    app.UseAuthentication();
    //app.MapControllers().RequireAuthorization();
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
}
catch (Exception exception)
{
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}

