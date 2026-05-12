using Microsoft.AspNetCore.Mvc;
using ProCleanArchitecture.Application.Interfaces;
using ProCleanArchitecture.Domain.Entities;

namespace ProCleanArchitecture.Web.Controllers;

public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _categoryService.GetAllAsync();
        return View(categories);
    }

    public IActionResult Create()
    {
        TempData["InfoMessage"] = "Categories are managed by the system and cannot be created manually.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Create(Category category)
    {
        await Task.CompletedTask;
        TempData["InfoMessage"] = "Categories are managed by the system and cannot be created manually.";
        return RedirectToAction(nameof(Index));
    }
}
