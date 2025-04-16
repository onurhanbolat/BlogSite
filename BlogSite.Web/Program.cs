using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BlogSite.Infrastructure.Data;
using BlogSite.Infrastructure.Repositories;
using BlogSite.Infrastructure.Services;
using BlogSite.Infrastructure.Seeders;
using BlogSite.Infrastructure.Services.Admin;
using BlogSite.Application.Interfaces;
using BlogSite.Application.Interfaces.ICategory;
using BlogSite.Application.Interfaces.IAdmin;
using BlogSite.Application.Interfaces.IBlog;
using BlogSite.Application.Interfaces.IComment;

var builder = WebApplication.CreateBuilder(args);

//Veritabanı Bağlantısı
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                      ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//Identity ve Rol Ayarları
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();

//Dependency Injection
builder.Services.AddScoped<IBlogWriterService, BlogWriterService>();
builder.Services.AddScoped<IBlogReaderService, BlogReaderService>();
builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.AddScoped<IAdminCategoryService, AdminCategoryService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ISelectListService, SelectListService>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ICommentService, CommentService>();


///Razor - MVC
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


var app = builder.Build();

//Middleware ve Seed İşlemleri
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();

    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    await TestDataSeeder.SeedRolesAndAdminAsync(context, userManager, roleManager);
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

//Routing ve Yetkilendirme
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Blog}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
