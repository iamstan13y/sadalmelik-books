using Microsoft.AspNetCore.Mvc;
using sadalmelik_books.Data.Services;
using sadalmelik_books.Data.ViewModels;

namespace sadalmelik_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private BooksService _booksService;

        public BooksController(BooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpPost("add-book")]
        public IActionResult AddBook([FromBody] BookVM book)
        {
            _booksService.AddBook(book);
            return Ok();
        }

        [HttpGet("get-all-books")]
        public IActionResult GetAllBooks()
        {
            var books = _booksService.GetAllBooks();
            return Ok(books);
        }

        [HttpGet("get-book-by-id/{id}")]
        public IActionResult GetBookById([FromRoute] int id)
        {
            var book = _booksService.GetBookById(id);
            return Ok(book);
        }

        [HttpPut("update-book/{id}")]
        public IActionResult UpdateBook(int id, [FromBody] BookVM book)
        {
            var result = _booksService.UpdateBook(id, book);
            return Ok(result);
        }

        [HttpDelete("delete-book/{id}")]
        public IActionResult DeleteBook(int id)
        {
            _booksService.DeleteBook(id);
            return Ok();
        }
    }
}
