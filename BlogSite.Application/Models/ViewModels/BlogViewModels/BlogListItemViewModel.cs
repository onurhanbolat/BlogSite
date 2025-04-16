namespace BlogSite.Application.Models.ViewModels.BlogViewModels
{
    public class BlogListItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Summary { get; set; }
        public string? AuthorUserName { get; set; }
        public string? CategoryName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
