using BlogSite.Application.Interfaces.IAdmin;
using BlogSite.Application.Models.ViewModels.BlogViewModels;
using BlogSite.Application.Models.ViewModels.UserViewModels;
using BlogSite.Domain.Authorization;
using BlogSite.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogSite.Infrastructure.Services.Admin
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<List<BlogAdminListViewModel>> GetAllBlogsAsync()
        {
            var blogs = await _context.Blogs.Include(b => b.Category).ToListAsync();
            var blogVMs = new List<BlogAdminListViewModel>();

            foreach (var blog in blogs)
            {
                var user = await _userManager.FindByIdAsync(blog.AuthorId);
                blogVMs.Add(new BlogAdminListViewModel
                {
                    Id = blog.Id,
                    Title = blog.Title,
                    CategoryName = blog.Category?.Name,
                    CreatedDate = blog.CreatedDate,
                    AuthorUserName = user?.UserName ?? "(Bilinmiyor)"
                });
            }

            return blogVMs;
        }

        public async Task DeleteBlogAsync(int blogId)
        {
            var blog = await _context.Blogs.FindAsync(blogId);
            if (blog != null)
            {
                _context.Blogs.Remove(blog);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<UserRoleViewModel>> GetAllUsersAsync(string currentUserId)
        {
            var users = await _userManager.Users.ToListAsync();
            var list = new List<UserRoleViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                list.Add(new UserRoleViewModel
                {
                    UserId = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    Roles = roles.ToList(),
                    IsDeletable = !roles.Contains(Roles.Admin) && user.Id != currentUserId
                });
            }

            return list;
        }

        public async Task ChangeUserRoleAsync(string userId, string newRole)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null || string.IsNullOrEmpty(newRole)) return;

            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);
            await _userManager.AddToRoleAsync(user, newRole);
        }

        public async Task DeleteUserAsync(string userId, string currentUserId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return;

            var isAdmin = await _userManager.IsInRoleAsync(user, Roles.Admin);
            if (user.Id == currentUserId || isAdmin) return;

            await _userManager.DeleteAsync(user);
        }
    }
}
