using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAppJob.Models;

namespace WebAppJob.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(UserModel user)
        {
            return View();
        }

    }
}