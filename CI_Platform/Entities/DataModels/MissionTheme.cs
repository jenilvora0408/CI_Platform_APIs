using Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.DataModels
{
    public class MissionTheme : BaseEntity<long>
    {
        [StringLength(255)]
        [Column("title")]
        public string Title { get; set; } = null!;

        [Column("status")]
        public MissionThemeStatus Status { get; set; }
    }
}
