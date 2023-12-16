using CanteenArduinoProject.ConnectionService;
using CanteenArduinoProject.Data;
using CanteenArduinoProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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

        public IActionResult Index(User user = null)
        {
            return View(user);
        }

        public IActionResult Privacy()
        {
            return View();
        }
          
        public async Task<IActionResult> Checking()
        {
            //IEnumerable<User> users = await _dataService.GetAllUsersAsync();
            //User findUser = await _dataService.GetUserByIdAsync("bd31152b");
            //string rfid = await _dataService.AsyncUID("bd31152b");
            //if (!string.IsNullOrEmpty(rfid))
            //{
            //    ViewData["ReceivedData"] = rfid;
            //}
            //else
            //{
            //    ViewData["ReceivedData"] = "Not Found";
            //}
             
            User findUser = await _dataService.AsyncUID();
            
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            User findUser = await _dataService.GetUserByUsernameAndPassAsync(user.Username, user.Password);
            return RedirectToAction("Index", findUser);
        }

        [HttpPost]
        public IActionResult SignUp(User user)
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    } 
}