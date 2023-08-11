using Microsoft.AspNetCore.Mvc;
using System;
using WebAppJob.Models;

namespace WebAppJob.Controllers
{
    [Route("[controller]")]
    public class JobController : Controller
    {

        [HttpGet("[action]/{id}")]
        public IActionResult DetailJob(int id)
        {
            JobViewModel jobViewModel = new JobViewModel();

            //return StatusCode(400, new { error = "Error desde controller" });

            return PartialView(jobViewModel);
        }

        [HttpGet]
        public PartialViewResult CreateJob()
        {
            JobViewModel jobViewModel = new JobViewModel();

            return PartialView(jobViewModel);
        }

        [HttpPost]
        public IActionResult CreateJob(JobViewModel jobView)
        {
            return Ok(jobView);
        }
    }
}
