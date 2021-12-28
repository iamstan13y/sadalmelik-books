using sadalmelik_books.Data.Models;
using sadalmelik_books.Data.Paging;
using sadalmelik_books.Data.ViewModels;
using sadalmelik_books.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace sadalmelik_books.Data.Services
{
    public class PublishersService
    {
        private AppDbContext _context;

        public PublishersService(AppDbContext context)
        {
            _context = context;
        }

        public Publisher AddPublisher(PublisherVM publisher)
        {
            if (StartsWithNumber(publisher.Name))
                throw new PublisherNameException("Name can't begin with number.", publisher.Name);

            var _publisher = new Publisher()
            {
                Name = publisher.Name
            };

            _context.Publishers.Add(_publisher);
            _context.SaveChanges();

            return _publisher;
        }

        public List<Publisher> GetAllPublishers(string sortBy, string searchString, int? pageNumber)
        {
            var publishers = _context.Publishers.OrderBy(n => n.Name).ToList();

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "name_desc":
                        publishers = publishers.OrderByDescending(n => n.Name).ToList();
                        break;
                    default:
                        break;
                }
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                publishers = publishers.Where(n => n.Name.Contains(searchString,
                    StringComparison.CurrentCultureIgnoreCase)).ToList();
            }

            int pageSize = 5;
            publishers = PaginatedList<Publisher>.Create(publishers.AsQueryable(), pageNumber ?? 1, pageSize);

            return publishers;
        } 

        public Publisher GetPublisherById(int id) => _context.Publishers.FirstOrDefault(x => x.Id == id);

        public PublisherWithBooksAndAuthorsVM GetPublisherData(int publisherId)
        {
            var _publisherData = _context.Publishers.Where(n => n.Id == publisherId)
                .Select(n => new PublisherWithBooksAndAuthorsVM()
                {
                    Name = n.Name,
                    Books = n.Books.Select(n => new BookAuthorVM()
                    {
                        BookName = n.Title,
                        BookAuthors = n.Book_Authors.Select(n => n.Author.Fullname).ToList()
                    }).ToList()
                }).FirstOrDefault();

            return _publisherData;
        }

        public void DeletePublisherById(int publisherId)
        {
            var publisher = _context.Publishers.FirstOrDefault(n => n.Id == publisherId);
            if (publisher != null)
            {
                _context.Publishers.Remove(publisher);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"The Publisher with Id: {publisherId} does not exist!");
            }
        }

        private bool StartsWithNumber(string name) => Regex.IsMatch(name, @"^\d");
    }
}