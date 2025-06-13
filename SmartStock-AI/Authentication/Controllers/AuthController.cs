using Microsoft.AspNetCore.Mvc;
using SmartStock_AI.Application.Authentication.DTOs;
using SmartStock_AI.Application.Authentication.Interfaces;
using SmartStock_AI.Filters;

namespace SmartStock_AI.Authentication.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var response = await _authService.LoginAsync(loginRequest);
            return Ok(response);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }
    
    [HttpPost("register")]
    [ServiceFilter(typeof(RegisterValidationFilter))]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            var response = await _authService.RegisterAsync(registerRequest);
            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message }); // 409 Conflict si el correo ya existe
        }
    }
}