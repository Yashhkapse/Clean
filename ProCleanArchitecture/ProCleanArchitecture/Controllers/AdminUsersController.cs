using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProCleanArchitecture.Application.Features.Users.Commands.CreateUser;
using ProCleanArchitecture.Application.Features.Users.Commands.DeleteUser;
using ProCleanArchitecture.Application.Features.Users.Commands.UpdateUser;
using ProCleanArchitecture.Application.Features.Users.Queries.GetUserById;
using ProCleanArchitecture.Application.Features.Users.Queries.GetUsers;
using ProCleanArchitecture.Web.ViewModels;

namespace ProCleanArchitecture.Web.Controllers;

[Route("Admin/Users")]
public class AdminUsersController : Controller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AdminUsersController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index(string? searchTerm, int pageNumber = 1, int pageSize = 10)
    {
        var result = await _mediator.Send(new GetUsersQuery(searchTerm, pageNumber, pageSize));
        var viewModel = new PagedListViewModel<UserListItemViewModel>
        {
            Items = _mapper.Map<IReadOnlyList<UserListItemViewModel>>(result.Items),
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
        var user = await _mediator.Send(new GetUserByIdQuery(id));
        if (user is null)
        {
            return NotFound();
        }

        return View(_mapper.Map<UserDetailsViewModel>(user));
    }

    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View(new UserFormViewModel());
    }

    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UserFormViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        try
        {
            var command = _mapper.Map<CreateUserCommand>(viewModel);
            await _mediator.Send(command);
            TempData["SuccessMessage"] = "User created successfully.";
            return RedirectToAction(nameof(Index));
        }
        catch (ValidationException exception)
        {
            AddValidationErrors(exception);
            return View(viewModel);
        }
    }

    [HttpGet("Edit/{id:guid}")]
    public async Task<IActionResult> Edit(Guid id)
    {
        var user = await _mediator.Send(new GetUserByIdQuery(id));
        if (user is null)
        {
            return NotFound();
        }

        return View(_mapper.Map<UserFormViewModel>(user));
    }

    [HttpPost("Edit/{id:guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, UserFormViewModel viewModel)
    {
        viewModel.Id = id;
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        try
        {
            var command = _mapper.Map<UpdateUserCommand>(viewModel);
            var updated = await _mediator.Send(command);
            if (!updated)
            {
                return NotFound();
            }

            TempData["SuccessMessage"] = "User updated successfully.";
            return RedirectToAction(nameof(Index));
        }
        catch (ValidationException exception)
        {
            AddValidationErrors(exception);
            return View(viewModel);
        }
    }

    [HttpGet("Delete/{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var user = await _mediator.Send(new GetUserByIdQuery(id));
        if (user is null)
        {
            return NotFound();
        }

        return View(_mapper.Map<UserDetailsViewModel>(user));
    }

    [HttpPost("Delete/{id:guid}")]
    [ActionName(nameof(Delete))]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var deleted = await _mediator.Send(new DeleteUserCommand(id));
        if (!deleted)
        {
            return NotFound();
        }

        TempData["SuccessMessage"] = "User deleted successfully.";
        return RedirectToAction(nameof(Index));
    }

    private void AddValidationErrors(ValidationException exception)
    {
        foreach (var error in exception.Errors)
        {
            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }
    }
}
