namespace BlogSite.Application.Models.ViewModels.CommentViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string AuthorUserName { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AuthorId { get; set; } 
    }
}
