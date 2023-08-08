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
        public IActionResult ApplyJob()
        {
            ApplyJobModel applyJobModel = new ApplyJobModel();
            applyJobModel.SurName = "Sanchez";
            applyJobModel.Birthdate = DateTime.Now.AddDays(-30);
            applyJobModel.Name = "Alfredo";

            return View(applyJobModel);
        }

        [HttpPost]
        public IActionResult RegisterApplyJob(ApplyJobModel applyJobModel)
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
        public IActionResult GetInformationJob([FromBody] Person body)
        {

            return Json(new { id = Guid.NewGuid(), Name = "Job Example" });
        }

    }

    public class Person
    {
        public string Id { get; set; }
    }

}