using BlogSite.Infrastructure.Data;
using BlogSite.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using BlogSite.Application.Interfaces;

namespace BlogSite.Infrastructure.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly ApplicationDbContext _context;

        public BlogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Blog>> GetAllAsync()
        {
            return await _context.Blogs
                .Include(b => b.Category)
                .ToListAsync();
        }

        public async Task<Blog?> GetByIdAsync(int id)
        {
            return await _context.Blogs
                .Include(b => b.Category)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task AddAsync(Blog blog)
        {
            await _context.Blogs.AddAsync(blog);
        }

        public async Task UpdateAsync(Blog blog)
        {
            _context.Blogs.Update(blog);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Blog blog)
        {
            _context.Blogs.Remove(blog);
            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
