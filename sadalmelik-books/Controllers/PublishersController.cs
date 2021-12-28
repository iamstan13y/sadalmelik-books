using Microsoft.AspNetCore.Mvc;
using sadalmelik_books.Data.Services;
using sadalmelik_books.Data.ViewModels;
using sadalmelik_books.Exceptions;
using System;

namespace sadalmelik_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        public PublishersService _publishersService;

        public PublishersController(PublishersService publishersService)
        {
            _publishersService = publishersService;
        }

        [HttpGet("get-all-publishers")]
        public IActionResult GetAllPublishers(string sortBy, string searchString, int pageNumber)
        {
            try
            {
                var result = _publishersService.GetAllPublishers(sortBy, searchString, pageNumber);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Sorry some crazy shit happened!");
            }
        }

        [HttpPost("add-publisher")]
        public IActionResult AddPublisher([FromBody] PublisherVM publisher)
        {
            try
            {
                var result = _publishersService.AddPublisher(publisher);
                return Created(nameof(AddPublisher), result);
            }
            catch (PublisherNameException ex)
            {
                return BadRequest($"{ex.Message}, Publisher Name: {ex.PublisherName}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-publisher/{id}")]
        public IActionResult GetPublisher(int id)
        {
            var response = _publishersService.GetPublisherById(id);

            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("get-publisher-data/{id}")]
        public IActionResult GetPublisherData(int id)
        {
            var response = _publishersService.GetPublisherData(id);
            return Ok(response);
        }

        [HttpDelete("delete-publisher/{id}")]
        public IActionResult DeletePublisher(int id)
        {
            try
            {
                _publishersService.DeletePublisherById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
