using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Wordfulness.Data;
using Wordfulness.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services
	.AddIdentity<User, IdentityRole<int>>(options =>
	{
		options.SignIn.RequireConfirmedAccount = true;
		options.Password.RequireDigit = false;
		options.Password.RequireUppercase = false;
		options.Password.RequireNonAlphanumeric = false;
		options.Password.RequiredLength = 1;
	})
	.AddEntityFrameworkStores<ApplicationDbContext>()
	.AddDefaultTokenProviders();
builder.Services.AddControllersWithViews();
builder.Services.ConfigureApplicationCookie(options =>
{
	options.LoginPath = new PathString("/Authentication/Login");
});

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
}
else
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
	"default",
	"{controller=Courses}/{action=Index}/{id?}");
ApplicationDbInitializer.Seed(app).Wait();

app.Run();