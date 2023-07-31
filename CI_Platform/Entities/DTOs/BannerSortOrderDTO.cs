using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class BannerSortOrderDTO
    {
        [Required]
        public int FromId { get; set; }

        [Required]
        public int ToId { get; set; }
    }
}