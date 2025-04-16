using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BlogSite.Application.Models.ViewModels.CommentViewModels;
using BlogSite.Application.Interfaces.IComment;

namespace BlogSite.Web.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly UserManager<IdentityUser> _userManager;

        public CommentController(ICommentService commentService, UserManager<IdentityUser> userManager)
        {
            _commentService = commentService;
            _userManager = userManager;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(CreateCommentViewModel model)
        {
            await _commentService.AddCommentAsync(model, User);
            return RedirectToAction("Details", "Blog", new { id = model.BlogId });
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(int commentId, int blogId)
        {
            var result = await _commentService.DeleteCommentAsync(commentId, User);
            if (!result)
                return Forbid();

            return RedirectToAction("Details", "Blog", new { id = blogId });
        }
    }
}
