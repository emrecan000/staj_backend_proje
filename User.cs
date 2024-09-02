using Newtonsoft.Json;

namespace staj_backend_proje.Models

{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        [JsonIgnore] 
        public ICollection<UserRole> UserRoles { get; set; }
    }
}