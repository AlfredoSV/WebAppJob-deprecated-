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
            try
            {
                return Ok(new { list = _catalogContext.Areas.ToList() });
            }
            catch (Exception ex)
            {
                return StatusCode(500,new { err = ex.Message });
            }
            
        }

        [HttpGet("[action]")]
        public IActionResult GetCompanies()
        {
            try
            {
                return Ok(new { list = _jobContext.Companies.ToList() });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { err = ex.Message });
            }
            
        }
    }
}
