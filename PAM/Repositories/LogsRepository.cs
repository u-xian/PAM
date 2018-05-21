using PAM.Models;
using System;

namespace PAM.Repositories
{
    public class LogsRepository
    {
        private readonly PAMDbContext _context;

        public LogsRepository()
        {
            _context = new PAMDbContext();
        }

        public void Save(string sender, string receiver, string request, string response)
        {
            Logs log = new Logs();

            log.sender = sender;
            log.receiver = receiver;
            log.request_xml = request;
            log.response_xml = response;
            log.created_at = DateTime.Now;

            _context.Logs.Add(log);
            _context.SaveChanges();
        }
    }
}