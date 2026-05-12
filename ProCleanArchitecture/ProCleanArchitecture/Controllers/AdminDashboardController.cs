using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProCleanArchitecture.Application.Features.Dashboard.Queries.GetDashboardStats;
using ProCleanArchitecture.Web.ViewModels;

namespace ProCleanArchitecture.Web.Controllers;

[Route("Admin/Dashboard")]
public class AdminDashboardController : Controller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AdminDashboardController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var stats = await _mediator.Send(new GetDashboardStatsQuery());
        var viewModel = _mapper.Map<AdminDashboardViewModel>(stats);

        return View(viewModel);
    }
}
