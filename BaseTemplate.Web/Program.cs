using BaseTemplate.Core.Interface;
using BaseTemplate.Core.Repository;
using BaseTemplate.Data.Contexts;
using BaseTemplate.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options => {
    options.Filters.Add(new ResponseCacheAttribute {
        Location = ResponseCacheLocation.None,
        NoStore = true
    });
});

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

using(var scope = app.Services.CreateScope()) {
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<RoleModel>>();
    string roleName = "System";
    if(!await roleManager.RoleExistsAsync(roleName)) {
        RoleModel role = new ();
        role.Name = roleName;
        await roleManager.CreateAsync(role);
    }
}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserModel>>();
    string userName = "System";
    string password = "Temporary01+";
    string firsName = "Default";
    string lasName  = "Default";
    string roleName = "System";
    if (await userManager.FindByNameAsync(userName) == null)
    {
        UserModel user = new UserModel();
        user.UserName  = userName;
        user.FirstName = firsName;
        user.LastName  = lasName;
        var u = await userManager.CreateAsync(user,password);
        if (u.Succeeded)
        {
            var r = await userManager.AddToRoleAsync(user, roleName);
            if (!r.Succeeded)
            {
                await userManager.DeleteAsync(user);
            }
        }
    }
}

app.Run();
