using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sadalmelik_books.ActionResults;
using sadalmelik_books.Data.Services;
using sadalmelik_books.Data.ViewModels;
using sadalmelik_books.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public CustomActionResult GetPublisher(int id)
        {
            var response = _publishersService.GetPublisherById(id);

            if (response != null)
            {
                var _responseObj = new CustomActionResultVM()
                {
                    Publisher = response
                };

                return new CustomActionResult(_responseObj);
            }
            else
            {
                var _responseObj = new CustomActionResultVM()
                {
                    Exception = new Exception("This came from Publishers controller.")
                };

                return new CustomActionResult(_responseObj);
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
