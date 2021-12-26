using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sadalmelik_books.Data.Models
{
    public class Publisher
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Navigation property
        public List<Book> Books { get; set; }
    }
}
