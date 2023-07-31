using Common.Constants;
using Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs;

public class VolunteerProfileFormDTO
{
    public long UserId { get; set; } = 0;

    [Required]
    [MaxLength(20, ErrorMessage = ModelStateConstant.FIRSTNAME_LENGTH_ERROR)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(20, ErrorMessage = ModelStateConstant.LASTNAME_LENGTH_ERROR)]
    public string LastName { get; set; } = null!;

    [Required]
    [RegularExpression(ModelStateConstant.REGEX_PHONE_NUMBER, ErrorMessage = ModelStateConstant.ERROR_PHONE_NUMBER)]
    public string PhoneNumber { get; set; } = null!;

    public string? EmployeeId { get; set; }

    public string? Department { get; set; }

    public string? ProfileText { get; set; }

    [EnumDataType(typeof(AvailabilityType), ErrorMessage = ModelStateConstant.AVAILABILITY)]
    public AvailabilityType Availability { get; set; }

    [MaxLength(4000, ErrorMessage = ModelStateConstant.REASON_TO_BE_VOLUNTEER)]
    public string? ReasonToBeVolunteer { get; set; }

    [Required(ErrorMessage = ModelStateConstant.ERROR_CITY)]
    public long CityId { get; set; }

    public List<int> VolunteerSkills { get; set; } = new List<int>();
}
