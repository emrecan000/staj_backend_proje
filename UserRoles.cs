using Newtonsoft.Json;

namespace staj_backend_proje.Models
{
    public class UserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
