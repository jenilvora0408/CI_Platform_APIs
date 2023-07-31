using Common.Enums;

namespace Entities.DTOs
{
    public class MissionApplicationInfoDTO
    {
        public long Id { get; set; }
        public string MissionTitle { get; set; } = null!;
        public string VolunteerName { get; set; } = null!;
        public MissionApplicationStatus Status { get; set; }
        public DateTimeOffset AppliedOn { get; set; }
    }
}
