using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogSite.Application.Interfaces.ICategory
{
    public interface ISelectListService
    {
        Task<List<SelectListItem>> GetCategorySelectListAsync();
    }
}

