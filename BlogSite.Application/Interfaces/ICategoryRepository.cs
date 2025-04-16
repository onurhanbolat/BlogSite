using BlogSite.Domain.Entities;

namespace BlogSite.Application.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task CreateAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Category category);
    }
}
