using Application.IServices;
using AutoMapper;
using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using WebAppJob.Models;

namespace WebAppJob.Controllers
{
    [Route("[controller]")]
    public class JobController : Controller
    {
        #region Abstract services

        private readonly IMapper _mapper;
        private readonly IServiceJob _serviceJob;
        private readonly ILogger<JobController> _logger;

        #endregion

        #region Constructors
        public JobController(IServiceJob serviceJob, IMapper autoMapper, ILogger<JobController> logger)
        {

            _logger = logger;
            _serviceJob = serviceJob;
            _mapper = autoMapper;
        }

        #endregion

        #region Return Partial View

        [HttpGet("[action]")]
        public PartialViewResult CreateJob() => PartialView("_CreateJob");

        [HttpGet("[action]")]
        public PartialViewResult GetDetailPartial() => PartialView("_DetailJob");

        [HttpGet("[action]")]
        public PartialViewResult EditJob() => PartialView("_EditJob");

        [HttpPost("[action]")]
        public async Task<PartialViewResult> ListJobs(int page, int pageSize, string searchText, string citySearch)
        {
            List<JobViewModel> jobsResult = new List<JobViewModel>();

            DtoPaginationViewModel<JobViewModel> dtoPaginationViewModel =
            new DtoPaginationViewModel<JobViewModel>();

            DtoResponse<List<Job>> response;

            if (string.IsNullOrEmpty(searchText))
                searchText = string.Empty;

            if (string.IsNullOrEmpty(citySearch))
                citySearch = string.Empty;

            response = await _serviceJob.GetJobsList(page, pageSize, searchText, citySearch);

            _mapper.Map(response.Data, jobsResult);

            dtoPaginationViewModel.Data = jobsResult;
            dtoPaginationViewModel.PaginationViewModel = new PaginationViewModel()
            {
                TotalCount = response.Count,
                PageSize = pageSize,
                PageIndex = page++
            };

            return PartialView("_ListJobs", dtoPaginationViewModel);

        }

        #endregion

        #region Return object json for fetch request

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> DetailJob(Guid id)
        {
            try
            {
                IQueryCollection context = HttpContext.Request.Query;
                JobViewModel jobViewModel = new JobViewModel();
                Job job = (await _serviceJob.GetDetailJob(id)).Data;

                _mapper.Map(job, jobViewModel);

                return Ok(jobViewModel);
            }
            catch (Exception ex)
            {
                Guid idError = Guid.NewGuid();
                _logger.LogError(idError + ex.Message);
                return BadRequest(new { Error = "A error was ocurred, the ticket is: " + idError });

            }

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateJob([FromBody] JobViewModel jobView)
        {
            try
            {

                DtoRequest<Job> dtoRequ = new DtoRequest<Job>();
                dtoRequ.Data = new Job();
                _mapper.Map(jobView, dtoRequ.Data);
                await _serviceJob.CreateJob(dtoRequ);

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
        public async Task<IActionResult> DeleteJob(Guid id)
        {
            try
            {
                await _serviceJob.DeleteJob(new DtoRequest<Guid> { Data = id });
                return Ok(new { message = "The job was delete successful" });
            }
            catch (Exception ex)
            {
                Guid idError = Guid.NewGuid();
                _logger.LogError(idError + ex.Message);
                return BadRequest(new { Error = "A error was ocurred, the ticket is: " + idError });

            }

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> EditJob([FromBody] JobViewModel jobView)
        {
            DtoRequest<Job> dtoRequ = new DtoRequest<Job>();
            dtoRequ.Data = new Job();
            Guid idError = Guid.NewGuid();
            try
            {
                if (ModelState.IsValid)
                {
                    _mapper.Map(jobView, dtoRequ.Data);
                    await _serviceJob.UpdateJob(dtoRequ);

                    return Ok(new { message = "The job was edited successful" });
                }
                else
                {
                    _logger.LogInformation(idError + @" The data of forma is not valid.");
                    return BadRequest(new { message = "Please, check your form data." });
                }


            }
            catch (Exception ex)
            {

                _logger.LogError(idError + ex.Message);
                return BadRequest(new { Error = "A error was ocurred, the ticket is: " + idError });

            }

        }

        #endregion

        #region Return View

        [HttpGet]
        public IActionResult ApplicationsJobs() => View();

        #endregion

    }
}
