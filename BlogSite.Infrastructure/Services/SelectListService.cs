using BlogSite.Application.Interfaces.ICategory;
using BlogSite.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class SelectListService : ISelectListService
{
    private readonly ApplicationDbContext _context;

    public SelectListService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<SelectListItem>> GetCategorySelectListAsync()
    {
        return await _context.Categories
            .OrderBy(c => c.Name)
            .Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToListAsync();
    }
}
