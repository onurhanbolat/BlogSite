using BlogSite.Application.Interfaces;
using BlogSite.Application.Interfaces.IBlog;
using BlogSite.Application.Models.ViewModels.BlogViewModels;
using BlogSite.Domain.Entities;

namespace BlogSite.Infrastructure.Services
{
    public class BlogWriterService : IBlogWriterService
    {
        private readonly IBlogRepository _repository;

        public BlogWriterService(IBlogRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateBlogAsync(CreateBlogViewModel model, string userId)
        {
            var blog = new Blog
            {
                Title = model.Title,
                Summary = model.Summary,
                Content = model.Content,
                CategoryId = model.CategoryId,
                CreatedDate = DateTime.Now,
                AuthorId = userId
            };

            await _repository.AddAsync(blog);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateBlogAsync(EditBlogViewModel model, string userId, bool isAdmin)
        {
            var blog = await _repository.GetByIdAsync(model.Id);
            if (blog == null || (blog.AuthorId != userId && !isAdmin))
                return;

            blog.Title = model.Title;
            blog.Summary = model.Summary;
            blog.Content = model.Content;
            blog.CategoryId = model.CategoryId;

            await _repository.UpdateAsync(blog);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteBlogAsync(int id, string userId, bool isAdmin)
        {
            var blog = await _repository.GetByIdAsync(id);
            if (blog == null || (blog.AuthorId != userId && !isAdmin))
                return;

            await _repository.DeleteAsync(blog);
            await _repository.SaveChangesAsync();
        }

        public async Task IncreaseViewCountAsync(int id)
        {
            var blog = await _repository.GetByIdAsync(id);
            if (blog != null)
            {
                blog.ViewCount++;
                await _repository.UpdateAsync(blog);
            }
        }
    }
}
