using Framework.Security2023;
using Framework.Security2023.Entities;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Persistence.Data;
using WebAppJob.Models;

namespace WebAppJob.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CatalogContext _context;
        private readonly IServiceUser _serviceUser;
		public HomeController(ILogger<HomeController> logger, CatalogContext context, IServiceUser serviceUser)
        {
			_serviceUser = serviceUser;
			_logger = logger;
            _context = context;
        }

        [HttpGet]     
        public IActionResult Index(string userName)
        {
            var str = SlqConnectionStr.Instance.SqlConnectionString;

			HttpContext.Session.SetString("User", "F672DD51-56CE-41F9-B5F4-81D80EEEFF41");
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult SeeMyInformation()
        {
            //Guid id = Guid.Parse(HttpContext.Session.GetString("User")??"");}
            Guid id = Guid.Parse("115B4AB8-978A-45B1-BBCE-54DE26B0C7BC");

            UserFkw userFkw = _serviceUser.GetUserById(id);

            UserViewModel userViewModel = UserViewModel.Create(userFkw.Id,userFkw.UserName,
                userFkw.DateCreated,"Example rol",userFkw.UserInformation.Name,
                userFkw.UserInformation.LastName,userFkw.UserInformation.Age,
                userFkw.UserInformation.Address,userFkw.UserInformation.Email);


            return View(userViewModel);
        }

    }


}