using Application.IServices;
using AutoMapper;
using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using WebAppJob.Models;

namespace WebAppJob.Controllers
{
    [Route("[controller]")]
    public class JobController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IServiceJob _serviceJob;
        private readonly ILogger<JobController> _logger;


        public JobController(IServiceJob serviceJob, IMapper autoMapper, ILogger<JobController> logger)
        {
            _logger = logger;
            _serviceJob = serviceJob;
            _mapper = autoMapper;
        }

        [HttpGet("[action]")]
        public IActionResult GetJobs()
        {
            try
            {

                IQueryCollection context = HttpContext.Request.Query;
                //Grid Js
                int limit = Int32.Parse(context["limit"].ToString());
                int page = Int32.Parse(context["page"].ToString());
                string texSearch = context["textSearch"].ToString().TrimEnd('?');

                List<JobViewModel> jobs = new List<JobViewModel>();
                DtoResponse<List<Job>> jobsResult = _serviceJob.GetJobsList(limit, (page * limit), texSearch);
                _mapper.Map(jobsResult.Data, jobs);

                #region Code for Datatable

                //string searchValue = context["search[value]"];
                //string lengtPage = context["length"];
                //string draw = context["draw"];
                //string start = context["start"];

                #endregion

                return Ok(new { results = jobs, count = jobsResult.Count });

            }
            catch (Exception ex)
            {
                Guid idError = Guid.NewGuid();
                _logger.LogError(idError + ex.Message);
                return BadRequest(new { Error = "A error was ocurred, the ticket is: " + idError });
            }


        }

        [HttpGet("[action]/{id}")]
        public IActionResult DetailJob(Guid id)
        {
            try
            {
                IQueryCollection context = HttpContext.Request.Query;
                JobViewModel jobViewModel = new JobViewModel();

                _mapper.Map(_serviceJob.GetDetailJob(id).Data, jobViewModel);

                return Ok(jobViewModel);
            }
            catch (Exception ex)
            {
                Guid idError = Guid.NewGuid();
                _logger.LogError(idError + ex.Message);
                return BadRequest(new { Error = "A error was ocurred, the ticket is: " + idError });

            }

        }

        [HttpGet("[action]")]
        public PartialViewResult CreateJob() => PartialView();

        [HttpGet("[action]")]
        public PartialViewResult GetDetail() => PartialView("DetailJob");

        [HttpPost("[action]")]
        public IActionResult CreateJob([FromBody] JobViewModel jobView)
        {
            try
            {
		
				DtoRequest<Job> dtoRequ = new DtoRequest<Job>();
                dtoRequ.Data = new Job();
                _mapper.Map(jobView, dtoRequ.Data);

                dtoRequ.Data.Id = Guid.NewGuid();
                dtoRequ.Data.IdUserCreated = Guid.NewGuid();
                dtoRequ.Data.UpdateDate = DateTime.Now;
                dtoRequ.Data.CreateDate = DateTime.Now;

                _serviceJob.CreateJob(dtoRequ);

                return Ok(new { message = "The job was created successful" });
            }
            catch (Exception ex)
            {
                Guid idError = Guid.NewGuid();
                _logger.LogError(idError + ex.Message);
                return BadRequest(new { Error = "A error was ocurred, the ticket is: " + idError });

            }

        }

        [HttpDelete("[action]/{id}")]
        public IActionResult DeleteJob(Guid id)
        {
            try
            {
                _serviceJob.DeleteJob(new DtoRequest<Guid> { Data = id });
                return Ok(new { message = "The job was delete successful" });
            }
            catch (Exception ex)
            {
                Guid idError = Guid.NewGuid();
                _logger.LogError(idError + ex.Message);
                return BadRequest(new { Error = "A error was ocurred, the ticket is: " + idError });

            }

        }



    }
}
