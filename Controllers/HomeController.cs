using CanteenArduinoProject.ConnectionService;
using CanteenArduinoProject.Data;
using CanteenArduinoProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;

namespace CanteenArduinoProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataService _dataService;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _dataService = new DataService();
        }

        public IActionResult Index(User user)
        {
            return View(user);
        }

        public IActionResult Privacy()
        {
            return View();
        }
          
        public async Task<IActionResult> Checking()
        {
            User user = await _dataService.AsyncUID();
             
            if (user != null)
            { 
                return RedirectToAction("ChoosenMenu", new { uid = user.Id });
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> CheckingOnCreation(User user)
        {  
            user.Id = await _dataService.GetUID();
            if (!_dataService.Exsist(user.Id) && user.Id != null)
            {
                _dataService.CreateUser(user);
                return RedirectToAction("Index", user);
            }
            else
            {
                return RedirectToAction("SignUp");
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult Waiting()
        {
            return View();
        }

        public IActionResult WaitingCreating(User user)
        {
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            User findUser = await _dataService.GetUserByUsernameAndPassAsync(user.Username, user.Password);
            if (findUser != null)
            {
                return RedirectToAction("Index", findUser);
            }
            else
            {
                return View(user);
            }
        }

        [HttpPost]
        public IActionResult SignUp(string FirstName, string LastName, string Username, string Password)
        {
            User user = new User {
                FirstName = FirstName, LastName= LastName, Username = Username,Password = Password
            };
             
            return RedirectToAction("WaitingCreating", user);
        }
         
        public IActionResult Menu(int day)
        {
            string dayOfWeek = null;
            switch (day)
            {
                case 1: dayOfWeek = "Понеделник"; break;
                case 2: dayOfWeek = "Вторник"; break;
                case 3: dayOfWeek = "Сряда"; break;
                case 4: dayOfWeek = "Четвъртък"; break;
                case 5: dayOfWeek = "Петък"; break;
            }
             
            return View(new MenuModel() { menuForTheDay = _dataService.GetMenuByDay(dayOfWeek), dayOfTheWeek = GetDayOfTheWeek(), dateOfTheDay = GetDayOfTheWeek()[dayOfWeek]});
        }

        [HttpPost]
        public IActionResult MenuChoise(int menu)
        {
            return RedirectToAction("Menu", new { day = 1 });
        }


        [HttpPost]
        public IActionResult ChooseDay(int day)
        {
            return RedirectToAction("Menu", new { day = day });
        }

        public IActionResult ChoosenMenu(string uid)
        {
            Menu? menu = _dataService.GetMenuOfUID(uid);

            return View(menu);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public Dictionary<string, string> GetDayOfTheWeek()
        {
            // Get the current date
            DateTime today = DateTime.Now;

            if (today.DayOfWeek == DayOfWeek.Saturday || today.DayOfWeek == DayOfWeek.Sunday)
            {
                today = today.AddDays(2);
            }

            // Find the start date (Monday) of the current week
            DateTime startDate = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);

            // Create a dictionary to store pairs of day and date
            Dictionary<string, string> dayDatePairs = new Dictionary<string, string>();

            // Print the dates for Monday to Friday of the current week
            Console.WriteLine("Dates for Monday to Friday of the current week:");

            for (int i = 0; i < 5; i++)
            {
                DateTime currentDate = startDate.AddDays(i);
                string dayOfWeek = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(currentDate.ToString("dddd", new CultureInfo("bg-BG")));
                dayDatePairs.Add(dayOfWeek, currentDate.ToString("yyyy-MM-dd"));
            }

            return dayDatePairs;
        } 
    } 
}