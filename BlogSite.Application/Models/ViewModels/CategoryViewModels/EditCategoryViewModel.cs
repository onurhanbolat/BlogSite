using System.ComponentModel.DataAnnotations;

namespace BlogSite.Application.Models.ViewModels.CategoryViewModels
{
    public class EditCategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kategori adı zorunludur.")]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
