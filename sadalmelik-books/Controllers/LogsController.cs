using Microsoft.AspNetCore.Mvc;
using sadalmelik_books.Data.Services;
using System;

namespace sadalmelik_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private LogsService _logsService;

        public LogsController(LogsService logsService)
        {
            _logsService = logsService;
        }

        [HttpGet("get-all-logs")]
        public IActionResult GetLogs()
        {
            try
            {
                var response = _logsService.GetAllLogs();
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest("Failed to query logs from database.");
            }
        }
    }
}
