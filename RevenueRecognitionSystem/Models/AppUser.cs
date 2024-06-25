using System.ComponentModel.DataAnnotations;

namespace RevenueRecognitionSystem.Models;

public class AppUser
{
    public enum AppUserType
    {
        Admin,
        User
    }

    public AppUser(string login, string password, AppUserType type, string salt, string refreshToken,
        DateTime refreshTokenExp)
    {
        Login = login;
        Password = password;
        Type = type;
        Salt = salt;
        RefreshToken = refreshToken;
        RefreshTokenExp = refreshTokenExp;
    }

    [Key] public int Id { get; set; }

    [Required] public string Login { get; set; }

    [Required] public string Password { get; set; }

    [Required] public AppUserType Type { get; set; }

    [Required] public string Salt { get; set; }

    [Required] public string RefreshToken { get; set; }

    [Required] public DateTime RefreshTokenExp { get; set; }
}