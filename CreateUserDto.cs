namespace staj_backend_proje.Models
{
    public class CreateUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}

