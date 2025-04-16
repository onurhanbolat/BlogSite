using BlogSite.Infrastructure.Data;
using BlogSite.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogSite.Infrastructure.Seeders
{
    public static class TestDataSeeder
    {
        public static async Task SeedRolesAndAdminAsync(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            await context.Database.MigrateAsync();

            var roles = new[] { "Admin", "User" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            string adminEmail = "admin@example.com";
            string adminPassword = "admin123";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    UserName = "admin",
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(adminUser, "Admin");
            }

            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category { Name = "Teknoloji" },
                    new Category { Name = "Spor" },
                    new Category { Name = "Yaşam" }
                );
                await context.SaveChangesAsync();
            }

            var userEmail = "user@example.com";
            var normalUser = await userManager.FindByEmailAsync(userEmail);

            if (normalUser == null)
            {
                normalUser = new IdentityUser
                {
                    UserName = "user",
                    Email = userEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(normalUser, "user123");
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(normalUser, "User");
            }

            if (!context.Blogs.Any())
            {
                var category = await context.Categories.FirstOrDefaultAsync();

                if (category != null && adminUser != null && normalUser != null)
                {
                    context.Blogs.AddRange(
                        new Blog
                        {
                            Title = "SOLID Prensibi",
                            Summary = "SOLID prensipleri yazılımda sürdürülebilirliği sağlar.",
                            Content = "Test içeriği...",
                            CreatedDate = DateTime.Now,
                            AuthorId = adminUser.Id,
                            CategoryId = category.Id
                        },
                        new Blog
                        {
                            Title = "Temiz Kod",
                            Summary = "Kod okunabilirliği önemlidir.",
                            Content = "Test içeriği...",
                            CreatedDate = DateTime.Now,
                            AuthorId = normalUser.Id,
                            CategoryId = category.Id
                        }
                    );

                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
