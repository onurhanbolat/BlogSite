using BlogSite.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogSite.Application.Interfaces.IAdmin
{
    public interface IAdminCategoryService
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task CreateAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(int id);
    }
}
