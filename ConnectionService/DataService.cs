using CanteenArduinoProject.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading;

namespace CanteenArduinoProject.ConnectionService
{
    public class DataService
    {
        private readonly CanteenContext _context;

        public DataService()
        {
            _context = new CanteenContext();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            return await _context.Users
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }
         
        public async Task<User> GetUserByUsernameAndPassAsync(string username, string password)
        {
            return await _context.Users
                .Where(x => x.Username == username && x.Password == password)
                .FirstOrDefaultAsync();
        }

        public async Task<User> AsyncUID()
        {
            var stopwatch = Stopwatch.StartNew();

            DateTime startTime = DateTime.Now;
            DateTime endTime = startTime + TimeSpan.FromSeconds(10);

            while (stopwatch.ElapsedMilliseconds < 10000)
            {
                 
                var readedId = _context.Readers
                    .Where(entry => entry.Time >= startTime && entry.Time <= endTime)
                    .Select(x => x.Uid)
                    .FirstOrDefault();

                var result = await _context.Users
                    .Where(u => u.Id == readedId) 
                    .FirstOrDefaultAsync();

                if (result != null)
                {
                    return result;  
                }
            }

            return null;
        }
    }
}
