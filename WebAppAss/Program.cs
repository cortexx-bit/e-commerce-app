using Microsoft.EntityFrameworkCore;
using System.Globalization;
using WebAppAss.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Configure the database context
builder.Services.AddDbContext<WebAppAssContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebAppAssContext")));

// Configure ASP.NET Core Identity for user authentication and roles
builder.Services.AddIdentity<IdentityUser, IdentityRole>(
    options =>
    {
        options.Stores.MaxLengthForKeys = 128;
    })
    .AddEntityFrameworkStores<WebAppAssContext>()
    .AddRoles<IdentityRole>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();


builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Set up global culture info to handle decimal formats consistently
var defaultCulture = new CultureInfo("en-GB");
CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
CultureInfo.DefaultThreadCurrentUICulture = defaultCulture;

// authorization policy
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdmins", policy => policy.RequireRole("Admin"));
});

builder.Services.AddRazorPages()
    .AddRazorPagesOptions(options =>
    {
        options.Conventions.AuthorizeFolder("/Admin", "RequireAdmins");
    });

builder.Services.AddScoped<IBasketService, BasketService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

// Ensure the database is created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<WebAppAssContext>();
    context.Database.EnsureCreated();
    DBInitializer.Initialize(context); // Seed the database
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();;
app.UseAuthorization();
app.MapRazorPages();

// Apply any pending database migrations and seed identity data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<WebAppAssContext>();
    context.Database.Migrate();
    var userMgr = services.GetRequiredService<UserManager<IdentityUser>>();
    var roleMgr = services.GetRequiredService<RoleManager<IdentityRole>>();
    IdentitySeedData.Initialize(context, userMgr, roleMgr).Wait();
}

// Start the app
app.Run();
