using BlogSite.Application.Models.ViewModels.BlogViewModels;

namespace BlogSite.Application.Interfaces.IBlog
{
    public interface IBlogWriterService
    {
        Task CreateBlogAsync(CreateBlogViewModel model, string userId);
        Task UpdateBlogAsync(EditBlogViewModel model, string userId, bool isAdmin);
        Task DeleteBlogAsync(int id, string userId, bool isAdmin);
        Task IncreaseViewCountAsync(int id);
    }
}
