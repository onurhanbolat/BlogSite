using BlogSite.Application;
using BlogSite.Domain.Entities;
using BlogSite.Infrastructure.Repositories;
using BlogSite.Application.Interfaces;
using BlogSite.Application.Interfaces.ICategory;

namespace BlogSite.Infrastructure.Services.Admin
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Task<List<Category>> GetAllAsync() => _categoryRepository.GetAllAsync();

        public Task<Category?> GetByIdAsync(int id) => _categoryRepository.GetByIdAsync(id);

        public Task CreateAsync(Category category) => _categoryRepository.CreateAsync(category);

        public Task UpdateAsync(Category category) => _categoryRepository.UpdateAsync(category);

        public async Task DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category != null)
                await _categoryRepository.DeleteAsync(category);
        }
    }
}
