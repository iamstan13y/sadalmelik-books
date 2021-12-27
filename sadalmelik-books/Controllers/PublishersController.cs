﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sadalmelik_books.Data.Services;
using sadalmelik_books.Data.ViewModels;
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
            var result = _publishersService.AddPublisher(publisher);
            return Created(nameof(AddPublisher), result);
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
            _publishersService.DeletePublisherById(id);
            return Ok();
        }
    }
}
