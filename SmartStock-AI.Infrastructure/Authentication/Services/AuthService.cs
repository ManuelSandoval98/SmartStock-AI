using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SmartStock_AI.Application.Authentication.DTOs;
using SmartStock_AI.Application.Authentication.Interfaces;
using SmartStock_AI.Application.UnitOfWork;
using SmartStock_AI.Domain;
using SmartStock_AI.Domain.Authentication.Entities;
using SmartStock_AI.Infrastructure.Authentication.Repositories;
using SmartStock_AI.Infrastructure.Helpers;
using SmartStock_AI.Infrastructure.Infrastructure.Persistence.Admin;
using SmartStock_AI.Infrastructure.Infrastructure.Persistence.Admin.Entities;
using SmartStock_AI.Infrastructure.UnitOfWork;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace SmartStock_AI.Infrastructure.Authentication.Services;

public class AuthService : IAuthService
{
        private readonly DatabaseCloneService _databaseCloneService;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INegocioLoginTrackingRepository _trackingRepository;
        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration, DatabaseCloneService databaseCloneService, INegocioLoginTrackingRepository trackingRepository)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _trackingRepository = trackingRepository;
            _databaseCloneService = databaseCloneService;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto loginRequest)
        {
            // 1️⃣ Verificar si el negocio existe
            var negocio = await _unitOfWork.NegocioRepository.GetByCorreoAsync(loginRequest.CorreoAdmin);
            if (negocio == null)
                throw new UnauthorizedAccessException("El correo no está registrado a ninguna cuenta.");

            // 2️⃣ Obtener o crear el tracking
            var tracking = await _trackingRepository.GetByCorreoAsync(loginRequest.CorreoAdmin);

            if (tracking == null)
            {
                tracking = new NegocioLoginTracking
                {
                    CorreoAdmin = loginRequest.CorreoAdmin,
                    IntentosFallidos = 0,
                    BloqueadoHasta = null
                };
                await _trackingRepository.AddAsync(tracking);
                await _trackingRepository.SaveChangesAsync();
            }

            // 3️⃣ Chequear si está bloqueado
            if (tracking.BloqueadoHasta.HasValue && tracking.BloqueadoHasta.Value > DateTime.UtcNow)
            {
                var minutosRestantes = (int)(tracking.BloqueadoHasta.Value - DateTime.UtcNow).TotalMinutes;
                throw new UnauthorizedAccessException($"Tu cuenta está bloqueada. Intenta en {minutosRestantes} minuto(s).");
            }

            // 4️⃣ Verificar contraseña
            bool isValidPassword = BCrypt.Net.BCrypt.Verify(loginRequest.Password, negocio.PasswordHash);

            if (!isValidPassword)
            {
                tracking.IntentosFallidos += 1;

                if (tracking.IntentosFallidos >= 3)
                {
                    tracking.BloqueadoHasta = DateTime.SpecifyKind(DateTime.UtcNow.AddMinutes(15),DateTimeKind.Unspecified);
                }

                await _trackingRepository.SaveChangesAsync();

                if (tracking.IntentosFallidos >= 3)
                    throw new UnauthorizedAccessException("Has excedido el número de intentos. Tu cuenta está bloqueada por 15 minutos.");
                else
                {
                    int intentosRestantes = 3 - (tracking.IntentosFallidos ?? 0);
                    throw new UnauthorizedAccessException($"Contraseña incorrecta. Te quedan {intentosRestantes} intento(s).");
                }
            }

            // 🔥 Contraseña correcta: resetea tracking
            tracking.IntentosFallidos = 0;
            tracking.BloqueadoHasta = null;
            await _trackingRepository.SaveChangesAsync();

            // 🔥 Login exitoso
            var token = GenerateJwtToken(negocio);

            return new AuthResponseDto
            {
                Token = token,
                NombreComercial = negocio.NombreComercial,
                CorreoAdmin = negocio.CorreoAdmin
            };
        }
    
        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto registerRequest)
        {
            var existingNegocio = await _unitOfWork.NegocioRepository.GetByCorreoAsync(registerRequest.CorreoAdmin);
            if (existingNegocio != null)
                throw new InvalidOperationException("El correo ya está registrado.");

            var existingNombre = await _unitOfWork.NegocioRepository.GetByNombreComercialAsync(registerRequest.NombreComercial);
            if (existingNombre != null)
                throw new InvalidOperationException("El nombre comercial ya está registrado.");

            var cleanDbName = StringHelper.RemoveSpecialCharacters(registerRequest.NombreComercial.ToLower().Replace(" ", "_"));

            var nuevoNegocio = new Negocio
            {
                NombreComercial = registerRequest.NombreComercial,
                CorreoAdmin = registerRequest.CorreoAdmin,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password),
                DbAsociada = $"db_{cleanDbName}",
                Estado = true,
                FechaCreacion = DateTime.Now
            };

            await _unitOfWork.NegocioRepository.AddAsync(nuevoNegocio);
            await _unitOfWork.SaveChangesAsync();

            _databaseCloneService.CloneDatabaseFromTemplate(nuevoNegocio.DbAsociada);

            var token = GenerateJwtToken(nuevoNegocio);

            return new AuthResponseDto
            {
                Token = token,
                NombreComercial = nuevoNegocio.NombreComercial,
                CorreoAdmin = nuevoNegocio.CorreoAdmin
            };
        }

        private string GenerateJwtToken(Negocio negocio)
        {
            var jwtKey = _configuration["Jwt:SecretKey"];
            var jwtIssuer = _configuration["Jwt:Issuer"];
            var jwtAudience = _configuration["Jwt:Audience"];
            var jwtExpiresMinutes = int.Parse(_configuration["Jwt:ExpiresMinutes"]);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, negocio.CorreoAdmin),
                new Claim("NegocioId", negocio.Id.ToString()),
                new Claim("NombreComercial", negocio.NombreComercial),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(jwtExpiresMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
}