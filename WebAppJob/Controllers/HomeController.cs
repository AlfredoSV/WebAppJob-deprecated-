using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Persistence.Data;
using System.Diagnostics;
using System.Linq;
using WebAppJob.Models;

namespace WebAppJob.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CatalogContext _context;
        public HomeController(ILogger<HomeController> logger, CatalogContext context)
        {
            
            _logger = logger;
            _context = context;
        }

        [HttpGet]     
        public IActionResult Index(string userName)
        {
            HttpContext.Session.SetString("User", "F672DD51-56CE-41F9-B5F4-81D80EEEFF41");
            return View();
        }

        [HttpGet]
        public PartialViewResult ApplyJob()
        {
            return PartialView();
        }

        [HttpGet]
        public IActionResult GetApplicationsJobs()
        {

            IQueryCollection context =  HttpContext.Request.Query;

            //string searchValue = context["search[value]"];
            //string lengtPage = context["length"];
            //string draw = context["draw"];
            //string start = context["start"];

            
            var res = _context.Areas.ToList();

            int limit = Int32.Parse(context["limit"].ToString());
            int page =  Int32.Parse(context["page"].ToString());
            string texSearch = context["textSearch"].ToString();
            
            List<ApplyJob> jobs = new List<ApplyJob>();

            jobs.Add(new ApplyJob() { Id = 1, Company ="Comapny1", DateApply = DateTime.Now.ToString(),Status="In process", Title="Title1" });
            jobs.Add(new ApplyJob() { Id = 2, Company = "Comapny2", DateApply = DateTime.Now.ToString(), Status = "Close", Title = "Title2" });
            jobs.Add(new ApplyJob() { Id = 1, Company = "Comapny1", DateApply = DateTime.Now.ToString(), Status = "In process", Title = "Title1" });
            jobs.Add(new ApplyJob() { Id = 2, Company = "Comapny2", DateApply = DateTime.Now.ToString(), Status = "Close", Title = "Title2" });

            jobs.Add(new ApplyJob() { Id = 1, Company = "Comapny1", DateApply = DateTime.Now.ToString(), Status = "In process", Title = "Title1" });
            jobs.Add(new ApplyJob() { Id = 2, Company = "Comapny2", DateApply = DateTime.Now.ToString(), Status = "Close", Title = "Title2" });


            return Ok(jobs);
        }


        [HttpPost("[action]")]
        public IActionResult RegisterApplyJob([FromBody]ApplyJobModel applyJobModel)
        {
            return Ok(new { Name = "Alfredo", Age = 26 });
        }



        [HttpPost]
        public IActionResult GetInformationJob()
        {

            return Json(new { id = Guid.NewGuid(), Name = "Job Example" });
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult SeeMyInformation()
        {
            Guid id = Guid.Parse(HttpContext.Session.GetString("User")??"");



            return View();
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