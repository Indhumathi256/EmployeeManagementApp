using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));  // Use SQL Server connection string

// Add Identity services for ApplicationUser and IdentityRole (for role-based auth)
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()  // Use ApplicationDbContext for identity data storage
    .AddDefaultTokenProviders();  // Add default token providers for password reset, etc.

// Add MVC controllers and views (for web app)
builder.Services.AddControllersWithViews();

// Add any other services your app needs (like dependency injection for services, etc.)

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();  // Show detailed error page in development
}
else
{
    app.UseExceptionHandler("/Home/Error");  // Show generic error page in production
    app.UseHsts();  // HTTP Strict Transport Security (HSTS) for secure connections
}

// Enforce HTTPS redirection (force HTTP requests to HTTPS)
app.UseHttpsRedirection();
app.UseStaticFiles();  // Enable serving static files like images, CSS, JS, etc.

// Setup routing (this is where URL patterns map to controllers)
app.UseRouting();

// Ensure the app uses authentication and authorization
app.UseAuthentication();  // This will handle user authentication (login/logout, etc.)
app.UseAuthorization();   // This will handle user authorization (access control, roles, etc.)

// Set up default route (if no controller/action is specified in the URL, use "Employee/Index")
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employee}/{action=Index}/{id?}");

// Start the web application
app.Run();
