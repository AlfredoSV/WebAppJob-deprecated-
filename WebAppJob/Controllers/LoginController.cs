
using Application.IServices;
using Domain.Entities;
using Framework.Security2023.Dtos;
using Framework.Security2023.Entities;
using Framework.Security2023.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using WebAppJob.Models;

namespace WebAppJob.Controllers
{
    public class LoginController : BaseController
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
                if (_serviceUser.UserExistByUserName(userName))
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
                string errorMessage = string.Empty;
                DtoLogin login = new DtoLogin() { UserName = user.UserName, Password = user.Password };

                if (ModelState.IsValid)
                {
                    DtoLoginResponse userLogin = _serviceLogin.Login(login);

                    if (userLogin.StatusLogin == StatusLogin.Ok)
                    {
                        SignIn(userLogin);
                        return RedirectToAction("Index", "Home", new { user.UserName });
                    }

                    switch (userLogin.StatusLogin)
                    {
                        case StatusLogin.UserOrPasswordIncorrect:

                            break;
                        case StatusLogin.UserBlocked:
                            errorMessage = "The user was blocked.";
                            break;
                        case StatusLogin.ExistSession:
                            errorMessage = "Exist a session.";
                            break;
                        case StatusLogin.TokenNotValid:
                            errorMessage = "The token was not correct.";
                            break;
                        case StatusLogin.RoleNotAssigned:
                            errorMessage = "Role not assigned.";
                            break;
                        default:
                            break;
                    }

                    ModelState.AddModelError("UserName", errorMessage);

                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("UserName", $"An error occurred while searching for the information of user, ticket:{SaveErrror(ex)}");
            }

            return View("LoginValidation", new UserModel { UserName = user.UserName });
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
            string urlBase = string.Empty;

            try
            {

                if (!ModelState.IsValid)
                    return View("ForgotPassword", userForgotPasswordRequest);


                if (!(_serviceUser.UserExistByUserName(userForgotPasswordRequest.UserName) &&
                    _serviceUser.UserExistByEmail(userForgotPasswordRequest.Email)))
                {
                    ModelState.AddModelError("UserName", "The user was not exist");
                    return View("ForgotPassword", userForgotPasswordRequest);
                }

                urlBase = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/ForgotPassword/ForgotPasswordChange";

                _serviceLogin.GenerateChangePasswordRequest(userForgotPasswordRequest.UserName,
                    urlBase);

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
            try
            {
                if (!ModelState.IsValid)
                    return View(viewName: "Register");

                if (_serviceUser.UserExistByEmail(userViewModel.Email) ||
                   _serviceUser.UserExistByUserName(userViewModel.UserName))
                {
                    ModelState.AddModelError("UserName", "The username or email was registred");
                    return View(viewName: "Register");
                }

                UserFkw userFkw = UserFkw.Create(
                    userViewModel.Name,
                    userViewModel.Password, 
                    Guid.NewGuid(), false,
                    Guid.Parse("35AE4DB6-0243-4B44-9B8B-C4E49ABD17E3"));

                userFkw.UserInformation = UserInformation.Create(
                    userViewModel.UserName, userViewModel.LastName,userViewModel.Age,
                    string.Empty,userViewModel.Email,Guid.NewGuid());   

                _serviceUser.CreateUser(userFkw,false);

                return RedirectToAction(actionName: "index", controllerName: "Login");


            }
            catch (Exception ex)
            {
                SaveErrror(ex);
                return View(viewName: "Register");
            }
           
        }

    }
}
