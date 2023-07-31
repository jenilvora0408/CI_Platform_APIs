using Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.DataModels
{
    public class City : BaseEntity<long>
    {
        [Column("country_id")]
        public long CountryId { get; set; }

        [StringLength(255)]
        [Column("name")]
        public string Name { get; set; } = null!;

        [Column("status")]
        public CityStatus Status { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; } = null!;
    }
}
