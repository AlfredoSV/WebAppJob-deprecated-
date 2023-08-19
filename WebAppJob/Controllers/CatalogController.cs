using Microsoft.AspNetCore.Mvc;
using Persistence.Data;

namespace WebAppJob.Controllers
{
    [Route("/")]
    public class CatalogController : Controller
    {
        private readonly CatalogContext _catalogContext;
        private readonly JobContext _jobContext;

        public CatalogController(CatalogContext catalogContext, JobContext jobContext)
        {
            _catalogContext = catalogContext;
            _jobContext = jobContext;
        }

        [HttpGet("[action]")]
        public IActionResult GetAreas()
        {
            return Ok(new { list = _catalogContext.Areas.ToList() });
        }

        [HttpGet("[action]")]
        public IActionResult GetCompanies()
        {
            return Ok(new { list = _jobContext.Companies.ToList()});
        }
    }
}
