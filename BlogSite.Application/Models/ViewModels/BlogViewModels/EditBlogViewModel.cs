using System.ComponentModel.DataAnnotations;

namespace BlogSite.Application.Models.ViewModels.BlogViewModels
{
    public class EditBlogViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Başlık zorunludur.")]
        public string Title { get; set; }

        public string? Summary { get; set; }

        [Required(ErrorMessage = "İçerik zorunludur.")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Kategori seçimi zorunludur.")]
        public int CategoryId { get; set; }
    }
}
