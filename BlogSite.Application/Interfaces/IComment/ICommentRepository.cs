using BlogSite.Domain.Entities;

namespace BlogSite.Application.Interfaces.IComment
{
    public interface ICommentRepository
    {
        Task AddAsync(Comment comment);
        Task<Comment?> GetByIdAsync(int id);
        Task DeleteAsync(Comment comment);
        Task<List<Comment>> GetCommentsByBlogIdAsync(int blogId);
    }
}
