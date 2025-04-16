using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BlogSite.Application.Models.ViewModels.BlogViewModels
{
    public class CreateBlogViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string? Summary { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }

        public List<SelectListItem> Categories { get; set; } = new(); 
    }
}
