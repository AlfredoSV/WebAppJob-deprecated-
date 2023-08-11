using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
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
            return View();
        }

        [HttpGet]
        public IActionResult GetApplicationsJobs()
        {

            IQueryCollection context =  HttpContext.Request.Query;

            string searchValue = context["search[value]"];
            string lengtPage = context["length"];
            string draw = context["draw"];
            string start = context["start"];

            List<ApplyJob> jobs = new List<ApplyJob>();

            jobs.Add(new ApplyJob() { Id = 1, Company ="Comapny1", DateApply = DateTime.Now.ToString(),Status="In process", Title="Title1" });
            jobs.Add(new ApplyJob() { Id = 2, Company = "Comapny2", DateApply = DateTime.Now.ToString(), Status = "Close", Title = "Title2" });
            jobs.Add(new ApplyJob() { Id = 1, Company = "Comapny1", DateApply = DateTime.Now.ToString(), Status = "In process", Title = "Title1" });
            jobs.Add(new ApplyJob() { Id = 2, Company = "Comapny2", DateApply = DateTime.Now.ToString(), Status = "Close", Title = "Title2" });

            jobs.Add(new ApplyJob() { Id = 1, Company = "Comapny1", DateApply = DateTime.Now.ToString(), Status = "In process", Title = "Title1" });
            jobs.Add(new ApplyJob() { Id = 2, Company = "Comapny2", DateApply = DateTime.Now.ToString(), Status = "Close", Title = "Title2" });

            jobs.Add(new ApplyJob() { Id = 1, Company = "Comapny1", DateApply = DateTime.Now.ToString(), Status = "In process", Title = "Title1" });
            jobs.Add(new ApplyJob() { Id = 2, Company = "Comapny2", DateApply = DateTime.Now.ToString(), Status = "Close", Title = "Title2" });

            jobs.Add(new ApplyJob() { Id = 1, Company = "Comapny1", DateApply = DateTime.Now.ToString(), Status = "In process", Title = "Title1" });
            jobs.Add(new ApplyJob() { Id = 2, Company = "Comapny2", DateApply = DateTime.Now.ToString(), Status = "Close", Title = "Title2" });

            jobs.Add(new ApplyJob() { Id = 1, Company = "Comapny1", DateApply = DateTime.Now.ToString(), Status = "In process", Title = "Title1" });
            jobs.Add(new ApplyJob() { Id = 2, Company = "Comapny2", DateApply = DateTime.Now.ToString(), Status = "Close", Title = "Title2" });

            jobs.Add(new ApplyJob() { Id = 1, Company = "Comapny1", DateApply = DateTime.Now.ToString(), Status = "In process", Title = "Title1" });
            jobs.Add(new ApplyJob() { Id = 2, Company = "Comapny2", DateApply = DateTime.Now.ToString(), Status = "Close", Title = "Title2" });

            jobs.Add(new ApplyJob() { Id = 1, Company = "Comapny1", DateApply = DateTime.Now.ToString(), Status = "In process", Title = "Title1" });
            jobs.Add(new ApplyJob() { Id = 2, Company = "Comapny2", DateApply = DateTime.Now.ToString(), Status = "Close", Title = "Title2" });

            jobs.Add(new ApplyJob() { Id = 1, Company = "Comapny1", DateApply = DateTime.Now.ToString(), Status = "In process", Title = "Title1" });
            jobs.Add(new ApplyJob() { Id = 2, Company = "Comapny2", DateApply = DateTime.Now.ToString(), Status = "Close", Title = "Title2" });

            jobs.Add(new ApplyJob() { Id = 1, Company = "Comapny1", DateApply = DateTime.Now.ToString(), Status = "In process", Title = "Title1" });
            jobs.Add(new ApplyJob() { Id = 2, Company = "Comapny2", DateApply = DateTime.Now.ToString(), Status = "Close", Title = "Title2" });

            jobs.Add(new ApplyJob() { Id = 1, Company = "Comapny1", DateApply = DateTime.Now.ToString(), Status = "In process", Title = "Title1" });
            jobs.Add(new ApplyJob() { Id = 2, Company = "Comapny2", DateApply = DateTime.Now.ToString(), Status = "Close", Title = "Title2" });


            return Ok(jobs);
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

        [HttpPost]
        public IActionResult GetInformationJob()
        {

            return Json(new { id = Guid.NewGuid(), Name = "Job Example" });
        }

    }

    public class ApplyJob
    {
        public int Id { get; set; }

        public string Company { get; set; }

        public string Title { get; set; }

        public string Status { get; set; }

        public string DateApply { get; set; }
    }

}