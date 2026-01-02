using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DrCoffee_BackEnd.Models;

namespace DrCoffee_BackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DiagnosticController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<DiagnosticController> _logger;

    public DiagnosticController(
        UserManager<ApplicationUser> userManager,
        ILogger<DiagnosticController> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    [HttpGet("check-admin")]
    public async Task<IActionResult> CheckAdmin()
    {
        try
        {
            var adminEmail = "admin@drcoffee.com";
            var user = await _userManager.FindByEmailAsync(adminEmail);
            
            if (user == null)
            {
                return Ok(new
                {
                    exists = false,
                    message = "Admin user does not exist in database",
                    email = adminEmail
                });
            }

            var roles = await _userManager.GetRolesAsync(user);
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            return Ok(new
            {
                exists = true,
                email = user.Email,
                userName = user.UserName,
                firstName = user.FirstName,
                lastName = user.LastName,
                emailConfirmed = user.EmailConfirmed,
                roles = roles,
                isAdmin = isAdmin,
                message = isAdmin ? "Admin user exists and has Admin role" : "Admin user exists but does not have Admin role"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking admin user");
            return StatusCode(500, new { message = "Error checking admin user", error = ex.Message });
        }
    }

    [HttpPost("reset-admin-password")]
    public async Task<IActionResult> ResetAdminPassword([FromBody] ResetPasswordDto dto)
    {
        try
        {
            var adminEmail = "admin@drcoffee.com";
            var adminPassword = dto.NewPassword ?? "Admin@123";
            var user = await _userManager.FindByEmailAsync(adminEmail);
            
            if (user == null)
            {
                // Create the user if it doesn't exist
                user = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FirstName = "Admin",
                    LastName = "User",
                    CreatedAt = DateTime.UtcNow
                };

                var createResult = await _userManager.CreateAsync(user, adminPassword);
                if (!createResult.Succeeded)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Failed to create admin user",
                        errors = createResult.Errors.Select(e => e.Description)
                    });
                }

                await _userManager.AddToRoleAsync(user, "Admin");
                _logger.LogInformation($"✅ Created admin user: {adminEmail}");

                return Ok(new
                {
                    success = true,
                    message = $"Admin user created successfully with password: {adminPassword}",
                    created = true
                });
            }

            // Reset password for existing user
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, adminPassword);

            if (result.Succeeded)
            {
                // Ensure user has Admin role
                if (!await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }

                _logger.LogInformation($"✅ Reset password for admin user: {adminEmail}");
                return Ok(new
                {
                    success = true,
                    message = $"Admin password reset successfully to: {adminPassword}",
                    created = false
                });
            }

            return BadRequest(new
            {
                success = false,
                message = "Failed to reset password",
                errors = result.Errors.Select(e => e.Description)
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error resetting admin password");
            return StatusCode(500, new { message = "Error resetting admin password", error = ex.Message });
        }
    }
}

public class ResetPasswordDto
{
    public string? NewPassword { get; set; }
}


