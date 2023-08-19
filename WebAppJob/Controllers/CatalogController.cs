using Microsoft.AspNetCore.Mvc;
using Persistence.Data;

namespace WebAppJob.Controllers
{
    [Route("/")]
    public class CatalogController : Controller
    {
        private readonly CatalogContext _catalogContext;

        public CatalogController(CatalogContext catalogContext) {
            _catalogContext = catalogContext;
        }

        [HttpGet("[action]")]
        public IActionResult GetAreas()
        {
            return Ok(new { list = _catalogContext.Areas.ToList() });
        }
    }
}
