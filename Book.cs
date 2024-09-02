namespace staj_backend_proje.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Author { get; set; }
        public bool IsActive { get; set; }
        public DateTime InsertDate { get; set; }
    }
}
