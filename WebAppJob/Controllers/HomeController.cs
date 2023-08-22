using Microsoft.AspNetCore.Mvc;
using Persistence.Data;

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


}