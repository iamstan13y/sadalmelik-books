using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using sadalmelik_books.Controllers;
using sadalmelik_books.Data;
using sadalmelik_books.Data.Models;
using sadalmelik_books.Data.Services;
using sadalmelik_books.Data.ViewModels;
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

        [Test, Order(1)]
        public void HTTPGET_GetAllPublishersTest()
        {
            IActionResult actionResult = publishersController.GetAllPublishers("name_desc", "Rulan", 1);

            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());

            var data = (actionResult as OkObjectResult).Value as List<Publisher>;

            Assert.That(data.First().Name, Is.EqualTo("Rulan Creative"));   
        }

        [Test, Order(2)]
        public void HTTPGET_GetPublisherByIdTest()
        {
            int publisherId = 1;
            
            IActionResult actionResult = publishersController.GetPublisher(publisherId);

            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());

            var data = (actionResult as OkObjectResult).Value as Publisher;
            Assert.That(data.Id, Is.EqualTo(1));
            Assert.That(data.Name, Is.EqualTo("Rulan Creative").IgnoreCase);
        }
        
        [Test, Order(3)]
        public void HTTPGET_GetPublisherByIdNotFoundTest()
        {
            int publisherId = 47;
            
            IActionResult actionResult = publishersController.GetPublisher(publisherId);

            Assert.That(actionResult, Is.TypeOf<NotFoundResult>());
        }

        [Test, Order(4)]
        public void HTTPPOST_AddPublisherTest()
        {
            var newPublisherVM = new PublisherVM()
            {
                Name = "Yet Another Publisher"
            };

            IActionResult actionResult = publishersController.AddPublisher(newPublisherVM);

            Assert.That(actionResult, Is.TypeOf<CreatedResult>());
        }

        [Test, Order(5)]
        public void HTTPPOST_AddPublisherBadRequestTest()
        {
            var newPublisherVM = new PublisherVM()
            {
                Name = "47 Another Publisher"
            };

            IActionResult actionResult = publishersController.AddPublisher(newPublisherVM);

            Assert.That(actionResult, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test, Order(6)]
        public void HTTPDELETE_DeletePublisherTest()
        {
            int publisherId = 3;

            IActionResult actionResult = publishersController.DeletePublisher(publisherId);

            Assert.That(actionResult, Is.TypeOf<OkResult>());
        }

        [Test, Order(7)]
        public void HTTPDELETE_DeletePublisherBadRequestTest()
        {
            int publisherId = 6;

            IActionResult actionResult = publishersController.DeletePublisher(publisherId);

            Assert.That(actionResult, Is.TypeOf<BadRequestObjectResult>());
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
