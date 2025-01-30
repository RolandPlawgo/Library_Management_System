using LibraryManagementSystem;
using LibraryManagementSystem.Authentication;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddDefaultIdentity<ApplicationUser>(options => {
//    options.SignIn.RequireConfirmedAccount = false;
//    options.SignIn.RequireConfirmedEmail = false;
//    })
//    .AddEntityFrameworkStores<ApplicationDbContext>()
//    .AddRoles<IdentityRole>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => {
	options.SignIn.RequireConfirmedAccount = false;
	options.SignIn.RequireConfirmedEmail = false;
    options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireUppercase = false;
	options.Password.RequireLowercase = false;
})
	.AddEntityFrameworkStores<ApplicationDbContext>()
	.AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

builder.Services.AddScoped(typeof(BookAvailability));

var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
	//var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
	//context.Database.Migrate();



	var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
	var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

	var user = new ApplicationUser()
	{
		FirstName = "Adam",
		LastName = "Nowak",
		PhoneNumber = "+48100200300",
		Email = "adamnowak@mail.com",
		UserName = "U123456"
	};
	await userManager.CreateAsync(user, "Qwerty1!");

    context.UserAccounts.Add(new UserAccountModel() { Id = user.Id, BorrowedBooks = new List<BookModel>(), ReservedBooks = new List<BookModel>() });
    context.SaveChanges();

    var admin = new ApplicationUser()
	{
		FirstName = "Jan",
		LastName = "Kowalski",
		PhoneNumber = "+48100100100",
		Email = "jankowalski@mail.com",
		UserName = "A123456"
	};
	await userManager.CreateAsync(admin, "Qwerty1!");

    context.UserAccounts.Add(new UserAccountModel() { Id = admin.Id, BorrowedBooks = new List<BookModel>(), ReservedBooks = new List<BookModel>() });
    context.SaveChanges();

    var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
	await roleManager.CreateAsync(new IdentityRole("Administrator"));
	await userManager.AddToRoleAsync(admin, "Administrator");
}

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
