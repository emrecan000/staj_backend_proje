using staj_backend_proje.Models;

namespace staj_backend_proje.Interfaces
{
    public interface IBookService
    {
        IEnumerable<Book> GetAllBooks();
        Book GetBookById(long id);
        void AddNewBook(Book newBook);
        Task<IEnumerable<Book>> GetAllBooksOrderByDateAsync();

        void DeleteBookById(long bookId);
    }
}
