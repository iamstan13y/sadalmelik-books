using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using sadalmelik_books.Data;
using sadalmelik_books.Data.Models;
using sadalmelik_books.Data.Services;
using System;
using System.Collections.Generic;

namespace Unit.Tests
{
    public class PublisherServiceTest
    {
        public static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "SadalmelikTests")
            .Options;

        AppDbContext context;
        PublishersService publisherService;

        [OneTimeSetUp]
        public void Setup()
        {
            context = new(dbContextOptions);
            context.Database.EnsureCreated();

            SeedDatabase();
            publisherService = new(context);
        }

        [Test]
        public void GetAllPublishers()
        {
            var result = publisherService.GetAllPublishers("", "", null);

            Assert.That(result.Count, Is.EqualTo(3));
        }

        [Test]
        public void GetPublisherById()
        {
            var result = publisherService.GetPublisherById(1);

            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Name, Is.EqualTo("Rulan Creative"));
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }

        private void SeedDatabase()
        {
            var publishers = new List<Publisher>()
            {
                new Publisher()
                {
                    Id = 1,
                    Name = "Rulan Creative"
                },
                
                new Publisher()
                {
                    Id = 2,
                    Name = "Stanton Digital"
                },

                new Publisher()
                {
                    Id = 3,
                    Name = "Juliet Designs"
                }
            };

            context.Publishers.AddRange(publishers);

            var authors = new List<Author>()
            {
                new Author()
                {
                    Id = 1,
                    Fullname = "Keith Stanley"
                },
                
                new Author()
                {
                    Id = 2,
                    Fullname = "Juliet Panashe"
                }
            };

            context.Authors.AddRange(authors);

            var books = new List<Book>()
            {
                new Book()
                {
                    Id = 1,
                    Title = "Romeo & Juliet",
                    Description = "A story of love.",
                    IsRead = false,
                    DateAdded = DateTime.Now.AddDays(-47),
                    CoverUrl = "https://....com/",
                    Genre = "Uthando",
                    PublisherId = 1,
                    Rate = 5
                },

                new Book()
                {
                    Id = 2,
                    Title = "Zibanibani My WIfe",
                    Description = "Just another story of love.",
                    IsRead = false,
                    DateAdded = DateTime.Now.AddDays(-15),
                    CoverUrl = "https://....com/",
                    Genre = "Uthando",
                    PublisherId = 1,
                    Rate = 4
                }
            };

            context.Books.AddRange(books);

            var book_authors = new List<Book_Author>()
            {
                new Book_Author()
                {
                    Id = 1,
                    AuthorId = 1,
                    BookId = 1
                },

                new Book_Author()
                {
                    Id = 2,
                    AuthorId = 1,
                    BookId = 2
                },

                new Book_Author()
                {
                    Id = 3,
                    AuthorId = 2,
                    BookId = 1
                }
            };

            context.Book_Authors.AddRange(book_authors);

            context.SaveChanges();
        }
    }
}