using Framework.Security2023;
using Framework.Security2023.Entities;
using Microsoft.AspNetCore.Mvc;
using WebAppJob.Models;
using static Framework.Security2023.Entities.Login;

namespace WebAppJob.Controllers
{
    public class LoginController : Controller
    {
        private readonly IServiceLogin _serviceLogin;
        private readonly IServiceUser _serviceUser;
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger, IServiceUser serviceUser, IServiceLogin serviceLogin)
        {
            this._serviceUser = serviceUser;
            this._serviceLogin = serviceLogin;
            this._logger = logger;
            _logger.LogDebug("NLog injected into LoginController");
        }

        [HttpGet]
        public IActionResult Index() => View();

        [HttpGet]
        public IActionResult Register() => View();

        [HttpGet]
        public IActionResult ForgotPassword() => View();

        [HttpPost]
        public IActionResult LoginUser(string userName)
        {
            try
            {
                if (_serviceUser.UserExist(userName))
                    return RedirectToAction("LoginValidation", "Login", new { userName });

                ModelState.AddModelError("UserName", "The user was not found");

            }
            catch (Exception e)
            {
                ModelState.AddModelError("UserName", "An error occurred while searching for the user");
            }

            return View("Index");

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

                Login login = Login.Create(user.UserName, user.Password);
                Login userLogin = _serviceLogin.Login(login);

                if (string.IsNullOrEmpty(user.Password))
                    ModelState.AddModelError("Password", "The credentials was not correct.");

                if (userLogin.StatusLog == StatusLogin.Ok)
                {
                    HttpContext.Session.SetString("User", userLogin.User.Id.ToString());
                    return RedirectToAction("Index", "Home", new { user.UserName });
                }
                else
                    ModelState.AddModelError("Password", "The credentials was not correct.");
            }
            catch (Exception)
            {

                ModelState.AddModelError("Password", "An error occurred while searching for the information of user");
            }

            return RedirectToAction("LoginValidation", new { username = user.UserName });

        }

        [HttpGet]
        public IActionResult Singout()
        {
            HttpContext.Session.Remove("User");

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ForgotPassword(UserForgotPasswordViewModel userForgotPassword)
        {
            bool valuesRequiredUserName = string.IsNullOrEmpty(userForgotPassword.UserName);
            bool valuesRequiredUserPassword =  string.IsNullOrEmpty(userForgotPassword.Email);

            try
            {
                if (valuesRequiredUserName)
                    ModelState.AddModelError("UserName", "The username was required.");

                if (valuesRequiredUserPassword)
                    ModelState.AddModelError("Email", "The email was required.");

                if (valuesRequiredUserName || valuesRequiredUserPassword)
                    return View("ForgotPassword", userForgotPassword);

                return RedirectToAction("Index", "Login");
            }
            catch (Exception)
            {

                ModelState.AddModelError("Password", "An error occurred while update for the password of user");
            }

            return RedirectToAction("ForgotPassword", new { username = userForgotPassword.UserName });

        }

    }
}
