using HouseRentingSystem.Data;
using HouseRentingSystem.Data.Models;
using HouseRentingSystem.Services.Data.Interfaces;
using HouseRentingSystem.Web.Infrastructure.ModelBinders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<HouseRentingDbContext>(options =>
    options.UseSqlServer(connectionString));



builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
// slagam edin object initializer za opciji za identity
    {
        options.SignIn.RequireConfirmedAccount = 
            builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
        options.Password.RequireLowercase =
            builder.Configuration.GetValue<bool>("Identity:Password:RequireLowercase");
        options.Password.RequireUppercase =
            builder.Configuration.GetValue<bool>("Identity:Password:RequireUppercase");
        options.Password.RequireNonAlphanumeric =
            builder.Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
        options.Password.RequiredLength =
            builder.Configuration.GetValue<int>("Identity:Password:RequireLength");
    })
    .AddEntityFrameworkStores<HouseRentingDbContext>();

builder.Services.AddApplicationServices(typeof(IHouseService));
//registrira controllerite
builder.Services
    .AddControllersWithViews()
    //The order is important because if we call default modelbinder for decimal first,
    //our custom modelbinder will not work, so we put 0 position after Insert
    //so our custom modelbinder will ovveride the default one
    .AddMvcOptions(options =>
    {
        options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
    });

//ot tuk red vazhi
WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    //kogato newo gramne vav browsera we mi zaredi cialata grewka
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error/500");
    app.UseStatusCodePagesWithRedirects("Home/Error?statusCode={0}");
    
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//ako niamam za momenta custom routeri
app.MapDefaultControllerRoute();
app.MapRazorPages();

app.Run();
