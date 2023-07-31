using Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class MissionCommentDTO
    {
        [Required]
        public long MissionId { get; set; }

        [Required]
        [StringLength(255)]
        public string Text { get; set; } = null!;

        public CommentStatus Status
        {
            get { return CommentStatus.PENDING; }
        }
    }
}
