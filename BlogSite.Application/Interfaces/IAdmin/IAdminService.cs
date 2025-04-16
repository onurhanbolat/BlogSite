using BlogSite.Application.Models.ViewModels.BlogViewModels;
using BlogSite.Application.Models.ViewModels.UserViewModels;

namespace BlogSite.Application.Interfaces.IAdmin
{
    public interface IAdminService
    {
        Task<List<BlogAdminListViewModel>> GetAllBlogsAsync();
        Task DeleteBlogAsync(int blogId);
        Task<List<UserRoleViewModel>> GetAllUsersAsync(string currentUserId);
        Task ChangeUserRoleAsync(string userId, string newRole);
        Task DeleteUserAsync(string userId, string currentUserId);
    }
}
