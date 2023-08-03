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

        [HttpGet]     
        public IActionResult Index(string userName)
        {
           
            return View();
        }

        [HttpGet]
        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Jobs()
        {
            return View();
        }

        public IActionResult DetailJob(Guid id) 
        {

            return View();
        
        }

        [HttpPost]
        public IActionResult GetInformationJob(string id)
        {

            return Json(new { id = Guid.NewGuid(), Name = "Job Example" });
        }

    }
}