using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DrCoffee_BackEnd.Models;
using DrCoffee_BackEnd.DTOs;

namespace DrCoffee_BackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthController>? _logger;

    public AuthController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration,
        ILogger<AuthController> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _logger = logger;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null)
        {
            _logger?.LogWarning($"Login attempt failed: User not found - {loginDto.Email}");
            return Unauthorized(new { message = "Invalid email or password" });
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        if (!result.Succeeded)
        {
            _logger?.LogWarning($"Login attempt failed: Invalid password for user - {loginDto.Email}");
            if (result.IsLockedOut)
                return Unauthorized(new { message = "Account is locked out" });
            if (result.IsNotAllowed)
                return Unauthorized(new { message = "Login not allowed for this account" });
            return Unauthorized(new { message = "Invalid email or password" });
        }

        var roles = await _userManager.GetRolesAsync(user);
        var token = GenerateJwtToken(user, roles);

        _logger?.LogInformation($"Successful login: {loginDto.Email} with roles: {string.Join(", ", roles)}");

        return Ok(new AuthResponseDto
        {
            Token = token,
            Email = user.Email!,
            FirstName = user.FirstName ?? string.Empty,
            LastName = user.LastName ?? string.Empty,
            Roles = roles.ToList(),
            ExpiresAt = DateTime.UtcNow.AddHours(24)
        });
    }

    private string GenerateJwtToken(ApplicationUser user, IList<string> roles)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}".Trim()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        // Add role claims
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not configured")));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(24),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}



