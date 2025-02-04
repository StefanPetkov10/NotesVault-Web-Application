using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NotesVaultApp.Data;
using NotesVaultApp.Data.Models;
using NotesVaultApp.DTOs;
using NotesVaultApp.Service.Data.Interfaces;

namespace NotesVaultApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly NotesVaultDbContext _context;
        private readonly IConfiguration _config;
        private readonly IAuthService _authService;

        public AuthController(NotesVaultDbContext context, IConfiguration config, IAuthService authService)
        {
            _context = context;
            _config = config;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (_context.Users.Any(u => u.Email == dto.Email))
                return BadRequest("Email already in use!");

            var user = new ApplicationUser
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = _authService.HashPassword(dto.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok("User registered!");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null || !_authService.VerifyPassword(dto.Password, user.PasswordHash))
                return Unauthorized("Invalid credentials!");

            // Генериране на JWT токен
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["JwtConfig:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                            new Claim(ClaimTypes.Name, user.Username),
                            new Claim(ClaimTypes.Email, user.Email)
                        }),
                Expires = DateTime.UtcNow.AddDays(_config.GetValue<int>("JwtConfig:ExpireDays")),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(new { token = tokenHandler.WriteToken(token) });
        }
    }
}
