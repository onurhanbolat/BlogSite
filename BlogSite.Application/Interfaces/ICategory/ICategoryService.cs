using BlogSite.Domain.Entities;

namespace BlogSite.Application.Interfaces.ICategory
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task CreateAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(int id);
    }
}
