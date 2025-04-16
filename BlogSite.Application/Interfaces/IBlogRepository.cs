using BlogSite.Domain.Entities;

namespace BlogSite.Application.Interfaces
{
    public interface IBlogRepository
    {
        Task<List<Blog>> GetAllAsync();
        Task<Blog?> GetByIdAsync(int id);
        Task AddAsync(Blog blog);
        Task UpdateAsync(Blog blog);
        Task DeleteAsync(Blog blog);
        Task SaveChangesAsync();
    }
}
