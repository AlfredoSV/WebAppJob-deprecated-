using Framework.Security2023;
using Framework.Security2023.Entities;
using Microsoft.AspNetCore.Mvc;
using WebAppJob.Models;
using Framework.Security2023.IServices;
using Domain.Entities;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Framework.Utilities2023.IServices;
using Framework.Utilities202.Entities;

namespace WebAppJob.Controllers
{

    public class HomeController : BaseController
    {
        private readonly IServiceUser _serviceUser;
        private readonly IServiceLogBook _logBook;
        public HomeController(IServiceUser serviceUser, IServiceLogBook serviceLogBook)
        {
            _serviceUser = serviceUser;
            _logBook = serviceLogBook;

        }

        //[AuthFilter]
        [HttpGet]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme,Roles ="Admin")]
        public IActionResult Index(string userName)
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
        [Authorize]
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
                Guid id;
                string idStr = HttpContext.Session.GetString("userId") ?? "";

                if (Guid.TryParse(idStr, out id))
                {
                    UserFkw userFkw = _serviceUser.GetUserById(id);

                    UserViewModel userViewModel = UserViewModel.Create(userFkw.Id, userFkw.UserName,
                        userFkw.DateCreated, Guid.NewGuid(), userFkw.UserInformation.Name,
                        userFkw.UserInformation.LastName, userFkw.UserInformation.Age,
                        userFkw.UserInformation.Address, userFkw.UserInformation.Email);


                    return View(userViewModel);
                }

                return RedirectToAction("Index","Login");

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