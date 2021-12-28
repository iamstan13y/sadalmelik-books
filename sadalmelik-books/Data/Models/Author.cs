using System.Collections.Generic;

namespace sadalmelik_books.Data.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Fullname { get; set; }

        public List<Book_Author> Book_Authors { get; set; }
    }
}
