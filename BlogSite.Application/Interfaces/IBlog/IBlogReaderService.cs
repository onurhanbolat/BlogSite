using BlogSite.Application.Models.ViewModels.BlogViewModels;
using BlogSite.Domain.Entities;

namespace BlogSite.Application.Interfaces.IBlog
{
    public interface IBlogReaderService
    {
        Task<List<BlogListItemViewModel>> GetBlogsAsync(int? categoryId);
        Task<BlogDetailViewModel?> GetBlogDetailsAsync(int id);
        Task<List<BlogListItemViewModel>> GetBlogsByAuthorIdAsync(string authorId);
        Task<Blog?> GetBlogEntityByIdAsync(int id);
    }
}
