using ConvaReload.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ConvaReload.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<User> _userManager;

    public RolesController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }
    
    [HttpPost ("Create")]
    public async Task<IActionResult> Create(string name)
    {
        IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
        return Ok(result);
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete(string id)
    {
        IdentityRole role = await _roleManager.FindByIdAsync(id);
        if (role != null)
            await _roleManager.DeleteAsync(role);
        return Ok(id);
    }

    [HttpPut("{userId}")]
    public async Task<IActionResult> Edit(string userId)
    {
        User user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var allRoles = _roleManager.Roles.ToList();

            Roles role = new Roles
            {
                UserId = user.Id,
                UserRoles = userRoles,
                AllRoles = allRoles
            };
            return Ok(role);
        }

        return Ok();
    }

    [HttpPut("Edit")]
    public async Task<IActionResult> Edit(string userId, List<string> roles)
    {
        User user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var addedRoles = roles.Except(userRoles);
            var removedRoles = userRoles.Except(roles);

            await _userManager.AddToRolesAsync(user, addedRoles);
            await _userManager.RemoveFromRolesAsync(user, removedRoles);
        }
        
        return Ok();
    }
}