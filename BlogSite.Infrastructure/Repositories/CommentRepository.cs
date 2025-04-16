using BlogSite.Application.Interfaces.IComment;
using BlogSite.Domain.Entities;
using BlogSite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogSite.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task DeleteAsync(Comment comment)
        {
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Comment>> GetCommentsByBlogIdAsync(int blogId)
        {
            return await _context.Comments
                .Where(c => c.BlogId == blogId)
                .OrderByDescending(c => c.CreatedDate)
                .ToListAsync();
        }
    }
}
