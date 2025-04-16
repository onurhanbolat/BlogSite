using BlogSite.Application.Interfaces;
using BlogSite.Application.Interfaces.IBlog;
using BlogSite.Application;
using BlogSite.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using BlogSite.Application.Models.ViewModels.BlogViewModels;

namespace BlogSite.Infrastructure.Services
{
    public class BlogReaderService : IBlogReaderService
    {
        private readonly IBlogRepository _repository;
        private readonly UserManager<IdentityUser> _userManager;

        public BlogReaderService(IBlogRepository repository, UserManager<IdentityUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public async Task<List<BlogListItemViewModel>> GetBlogsAsync(int? categoryId)
        {
            var blogs = await _repository.GetAllAsync();
            if (categoryId.HasValue)
                blogs = blogs.Where(b => b.CategoryId == categoryId.Value).ToList();

            return await MapToListItemViewModels(blogs);
        }

        public async Task<BlogDetailViewModel?> GetBlogDetailsAsync(int id)
        {
            var blog = await _repository.GetByIdAsync(id);
            if (blog == null)
                return null;

            var user = await _userManager.FindByIdAsync(blog.AuthorId);
            return new BlogDetailViewModel
            {
                Id = blog.Id,
                Title = blog.Title,
                Summary = blog.Summary,
                Content = blog.Content,
                CreatedDate = blog.CreatedDate,
                CategoryName = blog.Category?.Name,
                AuthorUserName = user?.UserName
            };
        }

        public async Task<List<BlogListItemViewModel>> GetBlogsByAuthorIdAsync(string authorId)
        {
            var blogs = await _repository.GetAllAsync();
            blogs = blogs.Where(b => b.AuthorId == authorId).ToList();
            return await MapToListItemViewModels(blogs);
        }

        private async Task<List<BlogListItemViewModel>> MapToListItemViewModels(List<Blog> blogs)
        {
            var list = new List<BlogListItemViewModel>();
            foreach (var blog in blogs)
            {
                var user = await _userManager.FindByIdAsync(blog.AuthorId);
                list.Add(new BlogListItemViewModel
                {
                    Id = blog.Id,
                    Title = blog.Title,
                    Summary = blog.Summary,
                    CreatedDate = blog.CreatedDate,
                    CategoryName = blog.Category?.Name,
                    AuthorUserName = user?.UserName
                });
            }

            return list;
        }

        public async Task<Blog?> GetBlogEntityByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
    }
}
