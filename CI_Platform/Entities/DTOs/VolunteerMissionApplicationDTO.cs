using Common.Constants;
using Common.Enums;
using Entities.CustomValidator;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs;

[CustomMissionApplicationValidation]
public class VolunteerMissionApplicationDTO
{
    [Required]
    public long MissionId { get; set; }
}