using BlogSite.Application.Models.ViewModels.CommentViewModels;

namespace BlogSite.Application.Models.ViewModels.BlogViewModels

{
    public class BlogDetailViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Summary { get; set; }
        public string Content { get; set; }
        public string? AuthorUserName { get; set; }
        public string? CategoryName { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<CommentViewModel> Comments { get; set; }
        public CreateCommentViewModel NewComment { get; set; }

    }
}
