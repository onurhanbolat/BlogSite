namespace BlogSite.Application.Models.ViewModels.BlogViewModels
{
    public class BlogAdminListViewModel
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AuthorUserName { get; set; }
    }

}
