using Application.IServices;
using Application.Services;
using AutoMapper;
using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using WebAppJob.Models;

namespace WebAppJob.Controllers
{
    
    public class JobController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IServiceJob _serviceJob;

        public JobController(IServiceJob serviceJob, IMapper autoMapper)
        {
            _serviceJob = serviceJob;
            _mapper = autoMapper;
        }

        [HttpGet]
        public IActionResult GetJobs()
        {

            IQueryCollection context = HttpContext.Request.Query;
            //Grid Js
            int limit = Int32.Parse(context["limit"].ToString());
            int page = Int32.Parse(context["page"].ToString());
            string texSearch = context["textSearch"].ToString().TrimEnd('?');

            List<JobViewModel> jobs = new List<JobViewModel>();
            DtoResponse<List<Job>> jobsResult = _serviceJob.GetJobsList(limit, (page * limit), texSearch);
            _mapper.Map(jobsResult.Data , jobs);
            //DataTable
            //string searchValue = context["search[value]"];
            //string lengtPage = context["length"];
            //string draw = context["draw"];
            //string start = context["start"];

            

            return Ok(new { results = jobs , count = jobsResult.Count});
        }

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
