using DriverApi.Data;
using DriverApi.Models;
using DriverApi.ModelsDtos;
using DriverApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace DriverApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<IdentityRole<string>> _roleManager;
        private readonly IJwtService _jwtService;
        private readonly MyDbContext _context;

        public UsersController(
            UserManager<Users> userManager,
            RoleManager<IdentityRole<string>> roleManager,
            IJwtService jwtService,
            MyDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
            _context = context;
        }

        // ========================= REGISTER =========================

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var emailExists =
                await _userManager.FindByEmailAsync(dto.Email);

            if (emailExists != null)
                return BadRequest("Email already exists");

            var user = new Users
            {
                UserName = dto.FullName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                CreatedAt = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false
            };

            var result =
                await _userManager.CreateAsync(
                    user,
                    dto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new ErrorRespone<object>
                {
                    Success = false,
                    Message = "Registration failed",
                    Errors = result.Errors
                        .Select(x => x.Description)
                        .ToList()
                });
            }
            await _userManager.AddToRoleAsync(user, "User");

            var token = await _jwtService.GenerateToken(user);

            var refreshToken = GenerateRefreshToken();

            _context.RefreshToken.Add(new RefreshToken
            {
                UserId = user.Id,
                Token = refreshToken,
                Expires = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            });

            await _context.SaveChangesAsync();

            return Ok(new AuthResponseDto
            {
                UserId = user.Id,
                UserName = user.UserName!,
                Email = user.Email!,
                Token = token,
                RefreshToken = refreshToken
            });
        }

        // ========================= LOGIN =========================

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(Userlogin dto)
        {
            var user =
                await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
                return Unauthorized("Invalid email or password");

            var passwordValid =
                await _userManager.CheckPasswordAsync(
                    user,
                    dto.Password);

            if (!passwordValid)
                return Unauthorized("Invalid email or password");

            user.LastLoginAt = DateTime.UtcNow;

            await _userManager.UpdateAsync(user);

            var token = await _jwtService.GenerateToken(user);

            var refreshToken = GenerateRefreshToken();

            _context.RefreshToken.Add(new RefreshToken
            {
                UserId = user.Id,
                Token = refreshToken,
                Expires = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            });

            await _context.SaveChangesAsync();

            return Ok(new AuthResponseDto
            {
                UserId = user.Id,
                UserName = user.UserName!,
                Email = user.Email!,
                Token = token,
                RefreshToken = refreshToken
            });
        }

        // ========================= REFRESH TOKEN =========================

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> RefreshToken(TokenRequestDto dto)
        {
            var storedToken =
                await _context.RefreshToken
                .FirstOrDefaultAsync(x => x.Token == dto.RefreshToken);

            if (storedToken == null)
                return Unauthorized("Invalid refresh token");

            if (storedToken.IsRevoked)
                return Unauthorized("Refresh token revoked");

            if (storedToken.Expires < DateTime.UtcNow)
                return Unauthorized("Refresh token expired");

            var user =
                await _userManager.FindByIdAsync(storedToken.UserId);

            if (user == null)
                return Unauthorized();

            // revoke old token
            storedToken.IsRevoked = true;

            var newAccessToken =
                await _jwtService.GenerateToken(user);

            var newRefreshToken =
                GenerateRefreshToken();

            _context.RefreshToken.Add(new RefreshToken
            {
                UserId = user.Id,
                Token = newRefreshToken,
                Expires = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            });

            await _context.SaveChangesAsync();

            return Ok(new AuthResponseDto
            {
                UserId = user.Id,
                UserName = user.UserName!,
                Email = user.Email!,
                Token = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

        // ========================= LOGOUT =========================

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout(TokenRequestDto dto)
        {
            var token =
                await _context.RefreshToken
                .FirstOrDefaultAsync(x => x.Token == dto.RefreshToken);

            if (token != null)
            {
                token.IsRevoked = true;
                await _context.SaveChangesAsync();
            }

            return Ok("Logged out");
        }

        // ========================= GET ALL USERS =========================

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userManager.Users
                .Select(x => new UserResponseDto
                {
                    UserId = x.Id,
                    UserName = x.UserName!,
                    Email = x.Email!,
                    PhoneNumber = x.PhoneNumber!,
                    CreatedAt = x.CreatedAt
                })
                .ToListAsync();

            return Ok(users);
        }

        // ========================= GET PROFILE =========================

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var userId =
                User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;

            if (userId == null)
                return Unauthorized();

            var user =
                await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound();

            return Ok(new UserResponseDto
            {
                UserId = user.Id,
                UserName = user.UserName!,
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber!,
                CreatedAt = user.CreatedAt
            });
        }

        // ========================= UPDATE PROFILE =========================

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateProfile(
            UpdateUserDto dto)
        {
            var userId =
                User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;

            if (userId == null)
                return Unauthorized();

            var user =
                await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound();

            user.UserName = dto.UserName;
            user.PhoneNumber = dto.PhoneNumber;

            await _userManager.UpdateAsync(user);

            return Ok("Profile updated");
        }

        // ========================= DELETE USER =========================

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user =
                await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            await _userManager.DeleteAsync(user);

            return Ok("User deleted");
        }

        // ========================= MAKE ADMIN =========================

        [Authorize(Roles = "Admin")]
        [HttpPost("{userId}")]
        public async Task<IActionResult> MakeAdmin(string userId)
        {
            var user =
                await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound();

            var alreadyAdmin =
                await _userManager.IsInRoleAsync(user, "Admin");

            if (alreadyAdmin)
                return BadRequest("User already admin");

            await _userManager.AddToRoleAsync(user, "Admin");

            return Ok("User is now admin");
        }

        // ========================= GENERATE REFRESH TOKEN =========================

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];

            using var rng = RandomNumberGenerator.Create();

            rng.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }

        [Authorize]
        [HttpGet]
        public IActionResult TestAuth()
        {
            Console.WriteLine("AUTH TYPE = " + User.Identity?.AuthenticationType);
            Console.WriteLine(
                "AUTHENTICATED = " +
                User.Identity?.IsAuthenticated);

            foreach (var claim in User.Claims)
            {
                Console.WriteLine(
                    $"{claim.Type} = {claim.Value}");
            }
            Console.WriteLine("=== CLAIMS ===");
            foreach (var c in User.Claims)
            {
                Console.WriteLine($"{c.Type} = {c.Value}");
            }

            Console.WriteLine("IsAuthenticated: " + User.Identity?.IsAuthenticated);
            Console.WriteLine("IsInRole(Admin): " + User.IsInRole("Admin"));
            return Ok();
        }
    }
}