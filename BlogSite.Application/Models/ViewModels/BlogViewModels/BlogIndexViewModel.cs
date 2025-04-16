using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogSite.Application.Models.ViewModels.BlogViewModels
{
    public class BlogIndexViewModel
    {
        public List<BlogListItemViewModel> Blogs { get; set; } = new();
        public List<SelectListItem> Categories { get; set; } = new();
        public int? SelectedCategoryId { get; set; }
    }
}
