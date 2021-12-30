using sadalmelik_books.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace sadalmelik_books.Data.Services
{
    public class LogsService
    {
        private AppDbContext _context;

        public LogsService(AppDbContext context)
        {
            _context = context;
        }

        public List<Log> GetAllLogs() => _context.Logs.ToList();
    }
}
