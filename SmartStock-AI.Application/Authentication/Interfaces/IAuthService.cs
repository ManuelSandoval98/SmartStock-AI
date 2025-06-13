using SmartStock_AI.Application.Authentication.DTOs;

namespace SmartStock_AI.Application.Authentication.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> LoginAsync(LoginRequestDto loginRequest);
    Task<AuthResponseDto> RegisterAsync(RegisterRequestDto registerRequest);
}