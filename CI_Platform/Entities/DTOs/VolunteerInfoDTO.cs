using Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs;

public class VolunteerInfoDTO : UserInfoDTO
{
    public string PhoneNumber { get; set; } = null!;

    public string? EmployeeId { get; set; }

    public string? Department { get; set; }

    public string? ProfileText { get; set; }

    public long CityId { get; set; }

    public string CityName { get; set; } = null!;

    public long CountryId { get; set; }

    public string CountryName { get; set; } = null!;
}