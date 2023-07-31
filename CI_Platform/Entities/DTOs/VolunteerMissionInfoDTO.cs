namespace Entities.DTOs
{
    public class VolunteerMissionInfoDTO : MissionDTO
    {
        public double AvgRating { get; set; } = 0;

        public bool IsFavourite { get; set; } = false;

        public bool HasApplied { get; set; } = false;
    }
}
