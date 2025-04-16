using BlogSite.Application.Models.ViewModels.CommentViewModels;
using BlogSite.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using BlogSite.Infrastructure.Repositories;
using BlogSite.Application.Interfaces.IComment;

namespace BlogSite.Infrastructure.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public CommentService(ICommentRepository commentRepository, UserManager<IdentityUser> userManager)
        {
            _commentRepository = commentRepository;
            _userManager = userManager;
        }

        public async Task AddCommentAsync(CreateCommentViewModel model, ClaimsPrincipal user)
        {
            var userId = _userManager.GetUserId(user);
            var userName = user.Identity?.Name;

            var comment = new Comment
            {
                BlogId = model.BlogId,
                AuthorId = userId,
                Content = model.Content,
                CreatedDate = DateTime.UtcNow
            };

            await _commentRepository.AddAsync(comment);
        }

        public async Task<bool> DeleteCommentAsync(int commentId, ClaimsPrincipal user)
        {
            var comment = await _commentRepository.GetByIdAsync(commentId);
            if (comment == null) return false;

            var userId = _userManager.GetUserId(user);
            var isAdmin = user.IsInRole("Admin");

            if (comment.AuthorId != userId && !isAdmin)
                return false;

            await _commentRepository.DeleteAsync(comment);
            return true;
        }

        public async Task<List<CommentViewModel>> GetCommentsByBlogIdAsync(int blogId)
        {
            var comments = await _commentRepository.GetCommentsByBlogIdAsync(blogId);
            var result = new List<CommentViewModel>();

            foreach (var c in comments)
            {
                var user = await _userManager.FindByIdAsync(c.AuthorId);
                result.Add(new CommentViewModel
                {
                    Id = c.Id,
                    AuthorId = c.AuthorId,
                    AuthorUserName = user?.UserName ?? "Kullanıcı",
                    Content = c.Content,
                    CreatedDate = c.CreatedDate
                });
            }

            return result;
        }

    }
}
