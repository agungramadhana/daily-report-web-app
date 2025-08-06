using DailyReport.Infrastructure;
using DailyReport.Application;
using DailyReport.Persistence;
using DailyReport.WebApp.Infrastructures;
using MediatR;
using Microsoft.EntityFrameworkCore;
using DailyReport.Application.Seeds;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<BaseResponseHandling>();
}).AddRazorRuntimeCompilation();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(opt =>
            {
                opt.LoginPath = "/Authentication/Index";
                opt.AccessDeniedPath = "/Authentication/Error";
            });

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

//add layer of Design Patter Clean Arcitecture
#region Depedency Injection
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddPersistance(builder.Configuration);
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseCustomExceptionHandler();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Authentication}/{action=Index}/{id?}");

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

using (var scope = app.Services.CreateScope())
{
    ApplicationDbContext appDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var dbContext = (DbContext)appDbContext;
    dbContext.Database.Migrate();

    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
    mediator.Send(new RoleSeedCommand()).GetAwaiter().GetResult();
    mediator.Send(new UserSeedCommand()).GetAwaiter().GetResult();
}

app.Run();
