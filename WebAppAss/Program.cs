using Microsoft.EntityFrameworkCore;
using System.Globalization;
using WebAppAss.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Configure the database context
builder.Services.AddDbContext<WebAppAssContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebAppAssContext")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Set up global culture info to handle decimal formats consistently (e.g., "en-GB")
var defaultCulture = new CultureInfo("en-GB");
CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
CultureInfo.DefaultThreadCurrentUICulture = defaultCulture;

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
app.UseAuthorization();
app.MapRazorPages();
app.Run();
