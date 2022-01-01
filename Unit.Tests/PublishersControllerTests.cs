using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using sadalmelik_books.Controllers;
using sadalmelik_books.Data;
using sadalmelik_books.Data.Models;
using sadalmelik_books.Data.Services;
using System.Collections.Generic;
using System.Linq;

namespace Unit.Tests
{
    public class PublishersControllerTests
    {
        public static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "SadalmelikControllerTests")
            .Options;

        AppDbContext context;
        PublishersService publisherService;
        PublishersController publishersController;

        [OneTimeSetUp]
        public void Setup()
        {
            context = new(dbContextOptions);
            context.Database.EnsureCreated();

            SeedDatabase();
            publisherService = new(context);
            publishersController = new(publisherService, new NullLogger<PublishersController>());
        }

        [Test]
        public void HTTPGET_GetAllPublishersTest()
        {
            IActionResult actionResult = publishersController.GetAllPublishers("name_desc", "Rulan", 1);

            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());

            var data = (actionResult as OkObjectResult).Value as List<Publisher>;

            Assert.That(data.First().Name, Is.EqualTo("Rulan Creative"));   
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

            context.SaveChanges();
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }

    }
}
