using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProCleanArchitecture.Application.Features.Categories.Queries.GetCategoryLookup;
using ProCleanArchitecture.Application.Features.Products.Commands.CreateProduct;
using ProCleanArchitecture.Application.Features.Products.Commands.DeleteProduct;
using ProCleanArchitecture.Application.Features.Products.Commands.UpdateProduct;
using ProCleanArchitecture.Application.Features.Products.Queries.GetProductById;
using ProCleanArchitecture.Application.Features.Products.Queries.GetProducts;
using ProCleanArchitecture.Web.ViewModels;

namespace ProCleanArchitecture.Web.Controllers;

[Route("Admin/Products")]
public class AdminProductsController : Controller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AdminProductsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index(string? searchTerm, int pageNumber = 1, int pageSize = 10)
    {
        var result = await _mediator.Send(new GetProductsQuery(searchTerm, pageNumber, pageSize));
        var viewModel = new PagedListViewModel<ProductListItemViewModel>
        {
            Items = _mapper.Map<IReadOnlyList<ProductListItemViewModel>>(result.Items),
            TotalCount = result.TotalCount,
            PageNumber = result.PageNumber,
            PageSize = result.PageSize,
            SearchTerm = result.SearchTerm
        };

        return View(viewModel);
    }

    [HttpGet("Details/{id:guid}")]
    public async Task<IActionResult> Details(Guid id)
    {
        var product = await _mediator.Send(new GetProductByIdQuery(id));
        if (product is null)
        {
            return NotFound();
        }

        return View(_mapper.Map<ProductDetailsViewModel>(product));
    }

    [HttpGet("Create")]
    public async Task<IActionResult> Create()
    {
        var viewModel = new ProductFormViewModel();
        await PopulateCategoriesAsync(viewModel);
        return View(viewModel);
    }

    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductFormViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            await PopulateCategoriesAsync(viewModel);
            return View(viewModel);
        }

        try
        {
            var command = _mapper.Map<CreateProductCommand>(viewModel);
            await _mediator.Send(command);
            TempData["SuccessMessage"] = "Product created successfully.";
            return RedirectToAction(nameof(Index));
        }
        catch (ValidationException exception)
        {
            AddValidationErrors(exception);
            await PopulateCategoriesAsync(viewModel);
            return View(viewModel);
        }
    }

    [HttpGet("Edit/{id:guid}")]
    public async Task<IActionResult> Edit(Guid id)
    {
        var product = await _mediator.Send(new GetProductByIdQuery(id));
        if (product is null)
        {
            return NotFound();
        }

        var viewModel = _mapper.Map<ProductFormViewModel>(product);
        await PopulateCategoriesAsync(viewModel);
        return View(viewModel);
    }

    [HttpPost("Edit/{id:guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, ProductFormViewModel viewModel)
    {
        viewModel.Id = id;
        if (!ModelState.IsValid)
        {
            await PopulateCategoriesAsync(viewModel);
            return View(viewModel);
        }

        try
        {
            var command = _mapper.Map<UpdateProductCommand>(viewModel);
            var updated = await _mediator.Send(command);
            if (!updated)
            {
                return NotFound();
            }

            TempData["SuccessMessage"] = "Product updated successfully.";
            return RedirectToAction(nameof(Index));
        }
        catch (ValidationException exception)
        {
            AddValidationErrors(exception);
            await PopulateCategoriesAsync(viewModel);
            return View(viewModel);
        }
    }

    [HttpGet("Delete/{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var product = await _mediator.Send(new GetProductByIdQuery(id));
        if (product is null)
        {
            return NotFound();
        }

        return View(_mapper.Map<ProductDetailsViewModel>(product));
    }

    [HttpPost("Delete/{id:guid}")]
    [ActionName(nameof(Delete))]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var deleted = await _mediator.Send(new DeleteProductCommand(id));
        if (!deleted)
        {
            return NotFound();
        }

        TempData["SuccessMessage"] = "Product deleted successfully.";
        return RedirectToAction(nameof(Index));
    }

    private async Task PopulateCategoriesAsync(ProductFormViewModel viewModel)
    {
        var categories = await _mediator.Send(new GetCategoryLookupQuery());
        viewModel.Categories = categories.Select(category => new SelectListItem
        {
            Value = category.Id.ToString(),
            Text = category.Name,
            Selected = category.Id == viewModel.CategoryId
        });
    }

    private void AddValidationErrors(ValidationException exception)
    {
        foreach (var error in exception.Errors)
        {
            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }
    }
}
