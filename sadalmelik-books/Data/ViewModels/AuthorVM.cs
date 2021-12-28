using System.Collections.Generic;

namespace sadalmelik_books.Data.ViewModels
{
    public class AuthorVM
    {
        public string Fullname { get; set; }
    }

    public class AuthorWithBooksVM
    {
        public string Fullname { get; set; }
        public List<string> BookTitles { get; set; }
    }
}
