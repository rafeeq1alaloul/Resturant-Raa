using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Resturant.Data;
using Resturant.Interfaces;
using Resturant.Models;
using Resturant.Repositories;
using Resturant.Services;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<YummyDbContext>(
    option =>
    {
        option.UseSqlServer(builder.Configuration.GetConnectionString("YummyConnection"));
    });


builder.Services.AddIdentity<User, IdentityRole>(option =>{
/*    option.Password.RequiredUniqueChars = 0;
    option.Password.RequiredLength = 12;
    option.Password.RequireNonAlphanumeric = true;
    option.Password.RequireDigit = true;
    option.Password.RequireLowercase = true;
    option.Password.RequireUppercase = true;*/
})
    .AddEntityFrameworkStores<YummyDbContext>()
    .AddDefaultTokenProviders();


builder.Services.ConfigureApplicationCookie(option => {
    option.LoginPath = "/UserAuth/Login";
});

/*builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option => {
    option.LoginPath = "/UserAuth/Login";
});*/

//builder.Services.AddHttpContextAccessor();


builder.Services.AddScoped<IChefsRepository, ChefsRepository>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IEventsRepository, EventsRepository>();
builder.Services.AddScoped<IShopingRepository, ShopingRepository>();
builder.Services.AddScoped<IImageServices, ImageServices>();


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

StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

});



app.Run();
