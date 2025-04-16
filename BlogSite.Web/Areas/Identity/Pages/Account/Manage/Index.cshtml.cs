using BlogSite.Application.Interfaces.IBlog;
using BlogSite.Application.Models.ViewModels.BlogViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlogSite.API.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IBlogReaderService _blogService;

        public IndexModel(UserManager<IdentityUser> userManager, IBlogReaderService blogService)
        {
            _userManager = userManager;
            _blogService = blogService;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public List<BlogListItemViewModel> Blogs { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound($"Kullanıcı yüklenemedi. ID: '{_userManager.GetUserId(User)}'.");

            Username = await _userManager.GetUserNameAsync(user);
            var userId = _userManager.GetUserId(User);

            Blogs = await _blogService.GetBlogsByAuthorIdAsync(userId);

            return Page();
        }
    }
}
