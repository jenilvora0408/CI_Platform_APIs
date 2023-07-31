using Common.Enums;

namespace Entities.DTOs;

public class UserInfoDTO
{
    public long Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Avatar { get; set; } = string.Empty;

    public UserStatus Status { get; set; }
}