using Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.DataModels
{
    public class CMSPage : BaseEntity<long>
    {
        [StringLength(128)]
        [Column("title")]
        public string Title { get; set; } = null!;

        [StringLength(255)]
        [Column("description")]
        public string Description { get; set; } = null!;

        [StringLength(64)]
        [Column("slug")]
        public string Slug { get; set; } = null!;

        [Column("status")]
        public CMSPageStatus Status { get; set; }
    }
}
