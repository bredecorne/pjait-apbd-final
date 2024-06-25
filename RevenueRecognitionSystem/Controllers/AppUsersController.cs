using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RevenueRecognitionSystem.Contexts;
using RevenueRecognitionSystem.Helpers;
using RevenueRecognitionSystem.Models;
using RevenueRecognitionSystem.Models.App;

namespace RevenueRecognitionSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppUsersController(IConfiguration configuration, RrsDbContext context) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAppUser(CreateAppUserDto userDto)
    {
        var (hashedPassword, salt) = SecurityHelpers.GetHashedPasswordAndSalt(userDto.Password);

        var user = new AppUser(
            userDto.Login,
            hashedPassword,
            AppUser.AppUserType.User,
            salt,
            SecurityHelpers.GenerateRefreshToken(),
            DateTime.UtcNow.AddDays(1)
        );

        context.AppUsers.Add(user);
        await context.SaveChangesAsync();

        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login(LoginRequestDto loginDto)
    {
        var user = context.AppUsers.FirstOrDefault(u => u.Login == loginDto.Login);
        if (user == null || user.Password != SecurityHelpers.GetHashedPasswordWithSalt(loginDto.Password, user.Salt))
            return Unauthorized(new { message = "Invalid login or password" });

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Login),
            new Claim(ClaimTypes.Role, user.Type.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            configuration["Jwt:Issuer"],
            configuration["Jwt:Audience"],
            claims,
            expires: DateTime.UtcNow.AddMinutes(10),
            signingCredentials: creds
        );

        user.RefreshToken = SecurityHelpers.GenerateRefreshToken();
        user.RefreshTokenExp = DateTime.UtcNow.AddDays(1);
        context.SaveChanges();

        return Ok(new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(token),
            refreshToken = user.RefreshToken
        });
    }

    [Authorize(AuthenticationSchemes = "IgnoreTokenExpirationScheme")]
    [HttpPost("refresh")]
    public IActionResult Refresh(RefreshTokenRequestDto refreshTokenDto)
    {
        var user = context.AppUsers.FirstOrDefault(u => u.RefreshToken == refreshTokenDto.RefreshToken);
        if (user == null || user.RefreshTokenExp < DateTime.UtcNow)
            return Unauthorized(new { message = "Invalid or expired refresh token" });

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Login),
            new Claim(ClaimTypes.Role, user.Type.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            configuration["Jwt:Issuer"],
            configuration["Jwt:Audience"],
            claims,
            expires: DateTime.UtcNow.AddMinutes(10),
            signingCredentials: creds
        );

        user.RefreshToken = SecurityHelpers.GenerateRefreshToken();
        user.RefreshTokenExp = DateTime.UtcNow.AddDays(1);
        context.SaveChanges();

        return Ok(new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(token),
            refreshToken = user.RefreshToken
        });
    }
    
    [Authorize(Roles = "Admin")]
    [HttpDelete]
    public async Task<IActionResult> DeleteAppUser(int id)
    {
        var user = await context.AppUsers.FindAsync(id);

        if (user == null) return NotFound();

        context.AppUsers.Remove(user);
        await context.SaveChangesAsync();

        return Ok();
    }
}