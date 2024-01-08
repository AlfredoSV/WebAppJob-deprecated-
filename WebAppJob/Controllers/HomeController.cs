using Framework.Security2023;
using Framework.Security2023.Entities;
using Microsoft.AspNetCore.Mvc;
using WebAppJob.Models;
using Framework.Security2023.IServices;
using Domain.Entities;

namespace WebAppJob.Controllers
{

    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServiceUser _serviceUser;
        public HomeController(ILogger<HomeController> logger, IServiceUser serviceUser)
        {
            _serviceUser = serviceUser;
            _logger = logger;
        }

        //[AuthFilter]
        [HttpGet]
        public IActionResult Index(string userName)
        {
            try
            {
                HttpContext.Session.SetString("User", "F672DD51-56CE-41F9-B5F4-81D80EEEFF41");
                return View();
            }
            catch (CommonException ex)
            {
                return ReturnResponseErrorCommon(ex);
            }
            catch (Exception ex)
            {
                return ReturnResponseIncorrect(ex);
            }

        }

        [HttpGet]
        public IActionResult About()
        {
            try
            {
                return View();
            }
            catch (CommonException ex)
            {
                return ReturnResponseErrorCommon(ex);
            }
            catch (Exception ex)
            {
                return ReturnResponseIncorrect(ex);
            }

        }

        [HttpGet]
        public IActionResult SeeMyInformation()
        {
            try
            {
                Guid id = Guid.Parse("115B4AB8-978A-45B1-BBCE-54DE26B0C7BC");

                UserFkw userFkw = _serviceUser.GetUserById(id);

                UserViewModel userViewModel = UserViewModel.Create(userFkw.Id, userFkw.UserName,
                    userFkw.DateCreated, Guid.NewGuid(), userFkw.UserInformation.Name,
                    userFkw.UserInformation.LastName, userFkw.UserInformation.Age,
                    userFkw.UserInformation.Address, userFkw.UserInformation.Email);


                return View(userViewModel);

            }
            catch (CommonException ex)
            {
                return ReturnResponseErrorCommon(ex);
            }
            catch (Exception ex)
            {
                return ReturnResponseIncorrect(ex);
            }


        }

    }


}