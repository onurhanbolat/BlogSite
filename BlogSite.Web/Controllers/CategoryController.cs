using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BlogSite.Domain.Authorization;
using BlogSite.Application.Interfaces.IAdmin;
using BlogSite.Application.Models.ViewModels.CategoryViewModels;

namespace BlogSite.Web.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class CategoryController : Controller
    {
        private readonly IAdminCategoryService _categoryService;

        public CategoryController(IAdminCategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();

            var viewModel = categories.Select(c => new CategoryListItemViewModel
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null) return NotFound();

            var model = new EditCategoryViewModel
            {
                Id = category.Id,
                Name = category.Name
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCategoryViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            await _categoryService.UpdateAsync(new Domain.Entities.Category
            {
                Id = model.Id,
                Name = model.Name
            });

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            await _categoryService.CreateAsync(new Domain.Entities.Category
            {
                Name = model.Name
            });

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
