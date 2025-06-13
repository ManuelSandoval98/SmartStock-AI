namespace SmartStock_AI.Application.Authentication.DTOs;

public class RegisterRequestDto
{
    public string NombreComercial { get; set; }
    public string CorreoAdmin { get; set; }
    public string Password { get; set; }
}