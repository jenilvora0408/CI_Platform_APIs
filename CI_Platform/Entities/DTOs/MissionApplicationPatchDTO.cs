using Common.Constants;
using Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class MissionApplicationPatchDTO
    {
        [Required(ErrorMessage = ExceptionMessage.MISSION_APPLICATION_STATUS_ERROR)]
        public MissionApplicationStatus Status { get; set; }
    }
}