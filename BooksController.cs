using Microsoft.AspNetCore.Mvc;
using staj_backend_proje.Interfaces;
using staj_backend_proje.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace staj_backend_proje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAllOrigins")]
    [Authorize(Roles = "Librarian,Admin,User")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = _bookService.GetAllBooks();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public IActionResult GetBookById(long id)
        {
            var book = _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        [Authorize(Roles = "Librarian,Admin")]
        public IActionResult AddNewBook(Book newBook)
        {
            _bookService.AddNewBook(newBook);
            return CreatedAtAction(nameof(GetBookById), new { id = newBook.Id }, newBook);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Librarian,Admin")]
        public IActionResult DeleteBookById(long id)
        {
            var book = _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            _bookService.DeleteBookById(id);
            return NoContent();
        }
    }
}
