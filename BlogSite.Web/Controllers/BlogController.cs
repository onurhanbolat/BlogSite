using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using BlogSite.Application.Models.ViewModels.CommentViewModels;
using BlogSite.Domain.Authorization;
using BlogSite.Application.Interfaces.ICategory;
using BlogSite.Application.Interfaces.IBlog;
using BlogSite.Application.Interfaces.IComment;
using BlogSite.Application.Models.ViewModels.BlogViewModels;

namespace BlogSite.Web.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        private readonly IBlogReaderService _blogReaderService;
        private readonly IBlogWriterService _blogWriterService;
        private readonly ISelectListService _selectListService;
        private readonly ICommentService _commentService;
        private readonly UserManager<IdentityUser> _userManager;

        public BlogController(
            IBlogReaderService blogReaderService,
            IBlogWriterService blogWriterService,
            ISelectListService selectListService,
            ICommentService commentService,
            UserManager<IdentityUser> userManager)
        {
            _blogReaderService = blogReaderService;
            _blogWriterService = blogWriterService;
            _selectListService = selectListService;
            _commentService = commentService;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(int? categoryId)
        {
            var blogs = await _blogReaderService.GetBlogsAsync(categoryId);
            var categories = await _selectListService.GetCategorySelectListAsync();

            return View(new BlogIndexViewModel
            {
                Blogs = blogs,
                Categories = categories,
                SelectedCategoryId = categoryId
            });
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var blog = await _blogReaderService.GetBlogDetailsAsync(id);
            if (blog == null)
                return NotFound();

            string cookieKey = $"Viewed_Blog_{id}";
            if (!Request.Cookies.ContainsKey(cookieKey))
            {
                await _blogWriterService.IncreaseViewCountAsync(id);

                CookieOptions options = new()
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(1)
                };
                Response.Cookies.Append(cookieKey, "true", options);
            }

            var comments = await _commentService.GetCommentsByBlogIdAsync(id);
            blog.Comments = comments;

            return View(blog);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _selectListService.GetCategorySelectListAsync();
            return View(new CreateBlogViewModel { Categories = categories });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBlogViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await _selectListService.GetCategorySelectListAsync();
                return View(model);
            }

            var userId = _userManager.GetUserId(User)!;
            await _blogWriterService.CreateBlogAsync(model, userId);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var blog = await _blogReaderService.GetBlogEntityByIdAsync(id);
            if (blog == null)
                return NotFound();

            var userId = _userManager.GetUserId(User);
            var isAdmin = User.IsInRole(Roles.Admin);

            if (blog.AuthorId != userId && !isAdmin)
                return Forbid();

            ViewBag.Categories = await _selectListService.GetCategorySelectListAsync();

            return View(new EditBlogViewModel
            {
                Id = blog.Id,
                Title = blog.Title,
                Summary = blog.Summary,
                Content = blog.Content,
                CategoryId = blog.CategoryId
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBlogViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _selectListService.GetCategorySelectListAsync();
                return View(model);
            }

            var userId = _userManager.GetUserId(User)!;
            var isAdmin = User.IsInRole(Roles.Admin);

            await _blogWriterService.UpdateBlogAsync(model, userId, isAdmin);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var blog = await _blogReaderService.GetBlogEntityByIdAsync(id);
            if (blog == null)
                return NotFound();

            var userId = _userManager.GetUserId(User);
            var isAdmin = User.IsInRole(Roles.Admin);

            if (blog.AuthorId != userId && !isAdmin)
                return Forbid();

            var blogDetail = await _blogReaderService.GetBlogDetailsAsync(id);
            return View(blogDetail);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = _userManager.GetUserId(User)!;
            var isAdmin = User.IsInRole(Roles.Admin);

            await _blogWriterService.DeleteBlogAsync(id, userId, isAdmin);
            return RedirectToAction(nameof(Index));
        }

    }
}
