using BlogSite.Application.Models.ViewModels.CommentViewModels;
using System.Security.Claims;

namespace BlogSite.Application.Interfaces.IComment
{
    public interface ICommentService
    {
        Task AddCommentAsync(CreateCommentViewModel model, ClaimsPrincipal user);
        Task<bool> DeleteCommentAsync(int commentId, ClaimsPrincipal user);
        Task<List<CommentViewModel>> GetCommentsByBlogIdAsync(int blogId);
    }
}
