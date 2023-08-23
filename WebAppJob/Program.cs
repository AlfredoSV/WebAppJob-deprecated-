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
using AutoMapper.Execution;
using AutoMapper;
using WebAppJob.Models;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Localization;

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


    builder.Services.AddTransient<IServiceLogin>(S => new ServiceLogin(""));
    builder.Services.AddTransient<IServiceJob,ServiceJob>();
  
  
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    string connectionStr = builder.Configuration.GetConnectionString("SqlServer");

    builder.Services.AddDbContext<CatalogContext>(options => options.UseSqlServer(connectionStr));
    builder.Services.AddDbContext<JobContext>(options => options.UseSqlServer(connectionStr));
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

