using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SmartStock_AI.Application.Authentication.DTOs;

namespace SmartStock_AI.Filters;

public class RegisterValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ActionArguments.TryGetValue("registerRequest", out var value) &&
            value is RegisterRequestDto register)
        {
            // Validar que no esté vacío
            if (string.IsNullOrWhiteSpace(register.NombreComercial) ||
                string.IsNullOrWhiteSpace(register.CorreoAdmin) ||
                string.IsNullOrWhiteSpace(register.Password))
            {
                context.Result = new BadRequestObjectResult(new { message = "Todos los campos son obligatorios." });
                return;
            }

            // Validar nombre comercial
            if (register.NombreComercial.Length < 2)
            {
                context.Result = new BadRequestObjectResult(new
                    { message = "El nombre comercial debe tener al menos 2 caracteres." });
                return;
            }

            // Validar correo
            if (!Regex.IsMatch(register.CorreoAdmin, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                context.Result = new BadRequestObjectResult(new { message = "El correo no tiene un formato válido." });
                return;
            }

            // Validar dominio del correo (hotmail, gmail o empresarial)
            if (!Regex.IsMatch(register.CorreoAdmin, @"@(hotmail|gmail)\.com$") &&
                !Regex.IsMatch(register.CorreoAdmin, @"@[\w\-]+\.[\w\-]+$"))
            {
                context.Result = new BadRequestObjectResult(new
                    { message = "El correo debe ser hotmail, gmail o empresarial." });
                return;
            }

            // Validar contraseña
            if (register.Password.Length < 8 ||
                !Regex.IsMatch(register.Password, @"[a-z]") ||
                !Regex.IsMatch(register.Password, @"[A-Z]") ||
                !Regex.IsMatch(register.Password, @"\d"))
            {
                context.Result = new BadRequestObjectResult(new
                {
                    message =
                        "La contraseña debe tener al menos 8 caracteres, incluyendo minúsculas, mayúsculas y números."
                });
                return;
            }
        }
    }
    public void OnActionExecuted(ActionExecutedContext context)
    {
        // No se necesita lógica post acción
    }
}