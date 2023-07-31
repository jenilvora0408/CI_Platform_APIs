using Common.Utils.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Common.Utils;
public class JwtHelper
{
    public static string GenerateToken(JwtSetting setting, SessionUserModel model)
    {
        if (setting == null)
            return string.Empty;

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(setting.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
                new Claim(ClaimTypes.Name, model.Id.ToString()),
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(ClaimTypes.Role, model.Type.ToString()),
                new Claim(ClaimTypes.NameIdentifier, model.Name),
        };

        var authToken = new JwtSecurityToken(
            setting.Issuer,
            setting.Audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(setting.ExpiryMinutes), // Default 5 mins, max 1 day
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(authToken);
    }

    public static string GenerateToken(ResetPasswordJwtSetting setting, ResetPasswordModel model)
    {
        if (setting == null)
            return string.Empty;

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(setting.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
                new Claim(ClaimTypes.NameIdentifier, model.Id.ToString()),
                new Claim("ValidTill", model.VaildTill.ToString("o")),
        };

        var authToken = new JwtSecurityToken(
            setting.Issuer,
            setting.Audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(setting.ExpiryMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(authToken);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public static ClaimsPrincipal? ValidateJwtToken(JwtSetting jwtSetting, string authToken)
    {
        JwtSecurityTokenHandler tokenHandler = new();

        byte[] key = Encoding.ASCII.GetBytes(jwtSetting.Key);

        TokenValidationParameters validationParameters = new()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidIssuer = jwtSetting.Issuer,
            //ValidAudience = jwtSetting.Audience,
            //ValidAudiences = new[] {jwtSetting.Audience},
            ClockSkew = TimeSpan.Zero
        };

        ClaimsPrincipal? principal = tokenHandler.ValidateToken(authToken, validationParameters, out var validatedToken);

        return principal;
    }

    public static DateTime GetTokenExpirationTime(string token)
    {
        JwtSecurityTokenHandler tokenHandler = new();
        JwtSecurityToken jwtSecurityToken = tokenHandler.ReadJwtToken(token);
        DateTime expirationTime = jwtSecurityToken.ValidTo;
        return expirationTime;
    }

    public static bool IsTokenExpired(string token)
    {
        DateTime expirationTime = GetTokenExpirationTime(token);
        return expirationTime <= DateTime.UtcNow;
    }
}
