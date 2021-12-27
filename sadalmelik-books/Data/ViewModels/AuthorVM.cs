using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
