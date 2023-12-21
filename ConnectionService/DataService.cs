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

        public async Task<User?> GetUserByIdAsync(string id)
        {
            return await _context.Users
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }
         
        public async Task<User?> GetUserByUsernameAndPassAsync(string username, string password)
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

        public async Task<string> GetUID()
        {
            var stopwatch = Stopwatch.StartNew();

            DateTime startTime = DateTime.Now;
            DateTime endTime = startTime + TimeSpan.FromSeconds(10);

            while (stopwatch.ElapsedMilliseconds < 10000)
            {

                var readedId = await _context.Readers
                    .Where(entry => entry.Time >= startTime && entry.Time <= endTime)
                    .Select(x => x.Uid)
                    .FirstOrDefaultAsync();

                if (readedId != null)
                {
                    return readedId;
                }
            }

            return null;
        }

        public bool Exsist(string id)
        {
            return _context.Users.Any(u => u.Id == id);
        }

        public void CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public Menu? GetMenuOfUID(string uid)
        {
             string currentDate = DateTime.Now.ToString("yyyy-MM-dd");

            return _context.Usermenus
                .Join(_context.Menus, um => um.MenuId, m => m.MenuId, (um, m) => new { um, m })
                .Where(joined => joined.um.UserId == uid && joined.um.Date.ToString() == currentDate)
                .Select(joined => joined.m)
                .FirstOrDefault();

        }

        public List<Menu?> GetMenuByDay(string dayOfWeek)
        {
            return _context.Menus.Where(x => x.Day == dayOfWeek).ToList();
        }
    }
}
