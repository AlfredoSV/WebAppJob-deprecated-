using Framework.Security2023;
using Framework.Security2023.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Reflection;
using WebAppJob.Models;
using Framework.Security2023.Entities; 
using System.Resources;
using Humanizer.Localisation;

namespace WebAppJob.Controllers
{
    public class LoginController : Controller
    {
        private readonly IServiceLogin _serviceLogin;
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger, IServiceLogin serviceLogin)
        {
            this._serviceLogin = serviceLogin;
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into LoginController");
        }

        public IActionResult Index()
        {
            string culture = CultureInfo.CurrentCulture.TextInfo.CultureName ;

      

            return View();
        }

        [HttpPost]
        public IActionResult LoginUser(string userName)
        {
            try
            {
                //if (this._serviceLogin.UserExist(userName))
                    return RedirectToAction("LoginValidation", "Login", new { userName });
               
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet]
        public IActionResult LoginValidation(string userName)
        {

            UserModel user = new UserModel();
            user.UserName = userName;
            return View(user);
        }

        [HttpPost]
        public IActionResult LoginValidation(UserModel user)
        {
            try
            {
                
                    //Login login = Login.Create(user.UserName, user.Password);
                    //if (this._serviceLogin.Login(login).StatusLog == StatusLogin.Ok)
                    return RedirectToAction("Index", "Home", new { user.UserName });

                

            }
            catch (Exception)
            {

                throw;
            }

            return RedirectToAction("LoginValidation", "Login", new { userName = user.UserName });

        }
    }
}
