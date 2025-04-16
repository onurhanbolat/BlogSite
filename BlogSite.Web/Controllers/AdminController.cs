using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BlogSite.Domain.Authorization;
using BlogSite.Application.Interfaces.IAdmin;

namespace BlogSite.Web.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<IActionResult> Index()
        {
            var blogs = await _adminService.GetAllBlogsAsync();
            return View(blogs);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            await _adminService.DeleteBlogAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Users()
        {
            var currentUserId = User.Identity?.Name ?? "";
            var userId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value
                         ?? User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;

            var users = await _adminService.GetAllUsersAsync(userId);
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRole(string userId, string newRole)
        {
            await _adminService.ChangeUserRoleAsync(userId, newRole);
            return RedirectToAction(nameof(Users));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var currentUserId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value
                                ?? User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;

            await _adminService.DeleteUserAsync(userId, currentUserId);
            return RedirectToAction(nameof(Users));
        }
    }
}
