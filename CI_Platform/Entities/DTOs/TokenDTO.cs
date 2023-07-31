using Common.Enums;

namespace Entities.DTOs;

public class TokenDTO
{
    public string Token { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;

    public UserType UserType { get; set; }
}
