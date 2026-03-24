using Application.Services;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebApp.Controllers;

// Controller för medlemskap.
[Authorize]
public class MembershipsController : Controller
{
    private readonly MembershipService _membershipService;
    private readonly UserManager<ApplicationUser> _userManager;

    public MembershipsController(
        MembershipService membershipService,
        UserManager<ApplicationUser> userManager)
    {
        _membershipService = membershipService;
        _userManager = userManager;
    }

    // Visar medlemskap för inloggad användare.
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user is null)
        {
            return Challenge();
        }

        var membership = await _membershipService.GetActiveMembershipAsync(user.Id, cancellationToken);

        return View(membership);
    }

    // Skapar medlemskap för inloggad användare.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user is null)
        {
            return Challenge();
        }

        var result = await _membershipService.CreateMembershipAsync(user.Id, cancellationToken);

        if (!result.IsSuccess)
        {
            TempData["MembershipError"] = result.Error;
            return RedirectToAction(nameof(Index));
        }

        TempData["MembershipSuccess"] = "Ditt medlemskap har skapats.";
        return RedirectToAction(nameof(Index));
    }
}