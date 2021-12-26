using sadalmelik_books.Data.Models;
using sadalmelik_books.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sadalmelik_books.Data.Services
{
    public class BooksService
    {
        private AppDbContext _context;

        public BooksService(AppDbContext context)
        {
            _context = context;
        }

        public void AddBook(BookVM book)
        {
            var _book = new Book()
            {
                Title = book.Title,
                Description = book.Description,
                CoverUrl = book.CoverUrl,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Rate = book.IsRead ? book.Rate : 0,
                Genre = book.Genre,
                DateAdded = DateTime.Now,
                PublisherId = book.PublisherId
            };

            _context.Books.Add(_book);
            _context.SaveChanges();

            foreach(var id in book.AuthorIds)
            {
                var _book_author = new Book_Author()
                {
                    BookId = _book.Id,
                    AuthorId = id
                };

                _context.Book_Authors.Add(_book_author);
                _context.SaveChanges();
            }
        }

        public List<Book> GetAllBooks() => _context.Books.ToList();
        public Book GetBookById(int bookId) => _context.Books.FirstOrDefault(x => x.Id == bookId);
        public Book UpdateBook(int bookId, BookVM book)
        {
            var _book = _context.Books.FirstOrDefault(x => x.Id == bookId);
            if (_book != null)
            {
                _book.Title = book.Title;
                _book.Description = book.Description;
                _book.CoverUrl = book.CoverUrl;
                _book.IsRead = book.IsRead;
                _book.DateRead = book.IsRead ? book.DateRead.Value : null;
                _book.Rate = book.IsRead ? book.Rate : 0;
                _book.Genre = book.Genre;
            }

            _context.SaveChanges();
            return _book;
        }

        public void DeleteBook(int bookId)
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == bookId);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }
    }
}
