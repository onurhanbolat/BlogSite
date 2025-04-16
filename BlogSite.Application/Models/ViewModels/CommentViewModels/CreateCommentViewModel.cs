using System.ComponentModel.DataAnnotations;

namespace BlogSite.Application.Models.ViewModels.CommentViewModels
{
    public class CreateCommentViewModel
    {
        public int BlogId { get; set; }

        [Required(ErrorMessage = "Yorum içeriği boş bırakılamaz.")]
        [StringLength(1000, MinimumLength = 1)]
        public string Content { get; set; }
    }
}
