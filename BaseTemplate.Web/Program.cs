using BaseTemplate.Core.Interface;
using BaseTemplate.Core.Repository;
using BaseTemplate.Data.Contexts;
using BaseTemplate.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IWorkUnit, WorkUnit>();

var connectionString = builder.Configuration.GetConnectionString("ConexionSQL") ?? 
    throw new InvalidOperationException("Connection string 'ConexionSQL' not found");
builder.Services
    .AddDbContext<AppDbContext>(option => option
    .UseSqlServer(connectionString));

builder.Services.AddIdentity<UserModel, RoleModel>()
    .AddRoles<RoleModel>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;
    options.Lockout.MaxFailedAccessAttempts  = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
});

builder.Services.ConfigureApplicationCookie(options => { 
    options.Cookie.Name ="ApplicationCookie";
    options.Cookie.HttpOnly = true;
    options.LoginPath ="/Account/Login";
    options.LogoutPath ="/Account/Login";
    options.AccessDeniedPath = "/Denied";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.SlidingExpiration = true;
});

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

app.Run();
