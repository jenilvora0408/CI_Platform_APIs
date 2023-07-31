using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.DataModels
{
    public class AuthToken : IdentityEntity<long>
    {
        [StringLength(1024)]
        [Column("token")]
        public string Token { get; set; } = null!;

        [Column("expires_on")]
        public DateTime ExpiresOn { get; set; }
    }
}
