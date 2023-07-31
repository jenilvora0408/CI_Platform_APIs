using Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs;
public class VolunteerDTO : UserDTO
{
    [Required]
    [RegularExpression(ModelStateConstant.REGEX_PHONE_NUMBER, ErrorMessage = ModelStateConstant.ERROR_PHONE_NUMBER)]
    public string PhoneNumber { get; set; } = null!;

    public string? EmployeeId { get; set; }

    public string? Department { get; set; }

    public string? ProfileText { get; set; }

    [Required(ErrorMessage = ModelStateConstant.ERROR_CITY)]
    public long CityId { get; set; }
}