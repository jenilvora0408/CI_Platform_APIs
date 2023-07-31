using Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.DataModels
{
    public class Country : BaseEntity<long>
    {
        [StringLength(255)]
        [Column("name")]
        public string Name { get; set; } = null!;

        [Column("status")]
        public CountryStatus Status { get; set; }
    }
}
