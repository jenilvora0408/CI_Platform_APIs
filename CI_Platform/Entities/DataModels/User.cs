using Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.DataModels
{
    public class User : TimestampedEntity<long>
    {
        [StringLength(20)]
        [Column("first_name")]
        public string FirstName { get; set; } = null!;

        [StringLength(20)]
        [Column("last_name")]
        public string LastName { get; set; } = null!;

        [StringLength(128)]
        [Column("email")]
        public string Email { get; set; } = null!;

        [StringLength(255)]
        [Column("password")]
        public string Password { get; set; } = null!;

        [StringLength(1024)]
        [Column("salt")]
        public string Salt { get; set; } = null!;

        [StringLength(1024)]
        [Column("token")]
        public string? Token { get; set; }

        [StringLength(1024)]
        [Column("avatar")]
        public string Avatar { get; set; } = null!;

        [Column("user_type")]
        public UserType UserType { get; set; }

        [Column("status")]
        public UserStatus Status { get; set; }
    }
}