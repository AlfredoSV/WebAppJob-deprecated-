
using Framework.Security2023.Entities;
using Framework.Security2023.IServices;
using Microsoft.AspNetCore.Mvc;
using WebAppJob.Models;

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
            catch (Exception)
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
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
        public IActionResult ForgotPasswordRequest(UserForgotPasswordRequestViewModel userForgotPasswordRequest)
        {
    
            try
            {

                if(!ModelState.IsValid)
                    return View("ForgotPassword", userForgotPasswordRequest);


                if (!_serviceUser.UserExist(userForgotPasswordRequest.UserName))
                {
                    ModelState.AddModelError("UserName", "The user was not exist");
                    return View("ForgotPassword", userForgotPasswordRequest);
                }


                return RedirectToAction("Index", "Login");
            }
            catch (Exception)
            {

                ModelState.AddModelError("Change Password", "An error occurred while update for the password of user");
            }

            return RedirectToAction("ForgotPassword", new { username = userForgotPasswordRequest.UserName });

        }

        [HttpGet("[action]/{id}/{idRequest}")]
        public IActionResult ForgotPasswordChange(Guid id, Guid idRequest)
        {

            try
            {


                return RedirectToAction("Index", "Login");
            }
            catch (Exception)
            {

                ModelState.AddModelError("Change Password", "An error occurred while update for the password of user");
            }

            return RedirectToAction("ForgotPassword", new { username = id });

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveNewUser(UserViewModelRegister userViewModel)
        {
            return null;
        }

    }
}
