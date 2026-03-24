using Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Presentation.WebApp.Controllers;

// Controller som visar alla pass.
public class ClassesController : Controller
{
    private readonly IAppDbContext _context;

    public ClassesController(IAppDbContext context)
    {
        _context = context;
    }

    // Visar lista med alla pass.
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var classes = await _context.GymClasses
            .OrderBy(x => x.Date)
            .ToListAsync(cancellationToken);

        return View(classes);
    }
}