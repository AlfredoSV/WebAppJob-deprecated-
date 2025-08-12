using Framework.Security2023.Entities;
using Microsoft.AspNetCore.Mvc;
using WebAppJob.Models;
using Framework.Security2023.IServices;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Framework.Utilities.IServices;
using WebAppJob.Filters;

namespace WebAppJob.Controllers
{

    public class HomeController : BaseController
    {
        private readonly IServiceUser _serviceUser;
        private readonly IServiceLogBook _logBook;
        public HomeController(IServiceUser serviceUser, IServiceLogBook serviceLogBook) : base(serviceLogBook)
        {
            _serviceUser = serviceUser;
            _logBook = serviceLogBook;

        }

        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        [AuthFilter]
        [HttpGet]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult Index(string userName)
        {
            try
            {
                ViewData["userName"] = HttpContext.Session.GetString("userName");
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
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> SeeMyInformation()
        {
            try
            {
                Guid id;
                string idStr = HttpContext.Session.GetString("userId") ?? "";

                if (Guid.TryParse(idStr, out id))
                {
                    UserFkw userFkw = await _serviceUser.GetUserById(id);

                    UserViewModel userViewModel = UserViewModel.Create(userFkw.Id, userFkw.UserName,
                        userFkw.DateCreated, Guid.NewGuid(), userFkw.UserInformation.Name,
                        userFkw.UserInformation.LastName, userFkw.UserInformation.Age,
                        userFkw.UserInformation.Address, userFkw.UserInformation.Email);


                    return View(userViewModel);
                }

                return RedirectToAction("Index", "Login");

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

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("/{id}")]
        public async Task<IActionResult> EditInformation(UserViewModel userViewModel, string id)
        {
            UserFkw userFkw = await _serviceUser.GetUserById(Guid.Parse(id));

            if (ModelState.IsValid)
            {
                userFkw.UserInformation.Address = userViewModel.Address;
                userFkw.UserInformation.Name = userViewModel.Name;
                userFkw.UserInformation.LastName = userViewModel.LastName;
                userFkw.UserInformation.Age = userViewModel.Age;
                userFkw.UserInformation.Email = userViewModel.Email;

                var result = _serviceUser.UpdateUser(userFkw);

                return RedirectToAction("SeeMyInformation", "Home");
            }           

            UserViewModel userViewModelRet = UserViewModel.Create(userFkw.Id, userFkw.UserName,
                userFkw.DateCreated, Guid.NewGuid(), userFkw.UserInformation.Name,
                userFkw.UserInformation.LastName, userFkw.UserInformation.Age,
                userFkw.UserInformation.Address, userFkw.UserInformation.Email);

            userViewModelRet.Address = userViewModel.Address;
            userViewModelRet.Name = userViewModel.Name;
            userViewModelRet.LastName = userViewModel.LastName;
            userViewModelRet.Age = userViewModel.Age;
            userViewModelRet.Email = userViewModel.Email;

            return View("SeeMyInformation", userViewModelRet);
        }
    }
}