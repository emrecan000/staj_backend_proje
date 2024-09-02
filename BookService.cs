using Microsoft.EntityFrameworkCore;
using staj_backend_proje.Interfaces;
using staj_backend_proje.Models;

namespace staj_backend_proje.Implements
{
    public class BookService : IBookService
    {
        private readonly BookContext _bookContext;

        public BookService(BookContext bookContext)
        {
            _bookContext = bookContext;
        }

        public void SoftDeleteBookById(long id)
        {
            var book = _bookContext.Books.Find(id);
            if (book != null)
            {
                book.IsActive = false;
                _bookContext.SaveChanges();
            }
        }

        public void DeleteBookById(long bookId)
        {
            var book = _bookContext.Books.Find(bookId);
            if (book != null)
            {
                _bookContext.Books.Remove(book);
                _bookContext.SaveChanges();
            }
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _bookContext.Books.Where(b => b.IsActive).ToList();
        }

        public Book GetBookById(long id)
        {
            return _bookContext.Books.Where(b => b.IsActive).FirstOrDefault(b => b.Id == id);
        }
     
        public void AddNewBook(Book newBook)
        {
            _bookContext.Books.Add(newBook);
            _bookContext.SaveChanges();
        }
        
        public async Task<IEnumerable<Book>> GetAllBooksOrderByDateAsync()
        {
            return await _bookContext.Books
                .OrderBy(b => b.InsertDate)
                .ToListAsync();
        }
    }
}
