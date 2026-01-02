using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DrCoffee_BackEnd.DTOs;
using DrCoffee_BackEnd.Repositories;

namespace DrCoffee_BackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomizationOptionsController : ControllerBase
{
    private readonly ICustomizationOptionRepository _repository;

    public CustomizationOptionsController(ICustomizationOptionRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Get all customization options (public endpoint for menu display)
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<CustomizationOptionResponseDto>>> GetCustomizationOptions()
    {
        var options = await _repository.GetAllCustomizationOptionsAsync();
        // Only return active options for public menu
        var activeOptions = options.Where(o => o.IsActive).ToList();
        return Ok(activeOptions);
    }

    /// <summary>
    /// Get all customization options (admin endpoint)
    /// </summary>
    [HttpGet("admin")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<ActionResult<IEnumerable<CustomizationOptionResponseDto>>> GetCustomizationOptionsAdmin()
    {
        var options = await _repository.GetAllCustomizationOptionsAsync();
        return Ok(options);
    }

    /// <summary>
    /// Get customization option by ID
    /// </summary>
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<ActionResult<CustomizationOptionResponseDto>> GetCustomizationOption(int id)
    {
        var option = await _repository.GetCustomizationOptionByIdAsync(id);
        if (option == null)
            return NotFound(new { message = "Customization option not found" });

        return Ok(option);
    }

    /// <summary>
    /// Create a new customization option
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<ActionResult<CustomizationOptionResponseDto>> CreateCustomizationOption([FromBody] CreateCustomizationOptionDto createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Check if option code already exists
        if (await _repository.OptionCodeExistsAsync(createDto.OptionCode))
            return Conflict(new { message = "Option code already exists" });

        var option = await _repository.CreateCustomizationOptionAsync(createDto);
        return CreatedAtAction(nameof(GetCustomizationOption), new { id = option.CustomizationOptionId }, option);
    }

    /// <summary>
    /// Update a customization option
    /// </summary>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<ActionResult<CustomizationOptionResponseDto>> UpdateCustomizationOption(int id, [FromBody] UpdateCustomizationOptionDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // If option code is being updated, check if it already exists
        if (!string.IsNullOrEmpty(updateDto.OptionCode))
        {
            if (await _repository.OptionCodeExistsAsync(updateDto.OptionCode, id))
                return Conflict(new { message = "Option code already exists" });
        }

        var option = await _repository.UpdateCustomizationOptionAsync(id, updateDto);
        if (option == null)
            return NotFound(new { message = "Customization option not found" });

        return Ok(option);
    }

    /// <summary>
    /// Delete a customization option
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> DeleteCustomizationOption(int id)
    {
        var deleted = await _repository.DeleteCustomizationOptionAsync(id);
        if (!deleted)
            return NotFound(new { message = "Customization option not found" });

        return NoContent();
    }
}

