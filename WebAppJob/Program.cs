using Framework.Security2023;
using NLog;
using NLog.Web;
using System;
using System.Configuration;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Application.Services;
using Application.IServices;
using Framework.Security2023.Services;
using Framework.Security2023.IServices;
using Domain.IRepositories;
using Domain.Repositories;
using Domain.Entities;
using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Authorization;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");
try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllersWithViews();

    string languaje = builder.Configuration.GetSection("DefaultLanguaje").Value;

    //CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(languaje);
   
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
	string connectionStr = builder.Configuration.GetConnectionString("SqlServer");
    string connectionStrFkw = builder.Configuration.GetConnectionString("SqlServerFkw"); 
	SlqConnectionStr.Instance.SqlConnectionString = connectionStrFkw;


	builder.Services.AddTransient<IServiceLogin, ServiceLogin>();
    builder.Services.AddTransient<IServiceJob,ServiceJob>();
	builder.Services.AddTransient<IServiceUser, ServiceUser>();
    builder.Services.AddTransient<IServiceCatalog<Area>, ServiceCatalogArea>();
    builder.Services.AddTransient<IServiceCatalog<Company>, ServiceCatalogCompany>();
    builder.Services.AddTransient<IRepositoryJob, RepositoryJob>();
    builder.Services.AddSession(options => {

        options.IdleTimeout = TimeSpan.FromMinutes(30); 

    });
	builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    builder.Services.AddDbContext<CatalogContext>(options => options.UseSqlServer(connectionStr));
    builder.Services.AddDbContextFactory<JobContext>(options => options.UseSqlServer(connectionStr));
    builder.Services.AddSession();
	builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
    builder.Services.AddMvc().AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();
	var app = builder.Build();
    
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

