
using Application.IServices;
using DocumentFormat.OpenXml.Spreadsheet;
using Domain.Entities;
using Framework.Security2023.Dtos;
using Framework.Security2023.Entities;
using Framework.Security2023.IServices;
using Framework.Utilities202.Entities;
using Framework.Utilities2023.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Security.Claims;
using WebAppJob.Models;

namespace WebAppJob.Controllers
{
    public class LoginController : BaseController
    {
        private readonly IServiceLogin _serviceLogin;
        private readonly IServiceUser _serviceUser;
        private readonly IServiceLogBook _serviceLogBook;
        public LoginController(IServiceUser serviceUser, IServiceLogin serviceLogin, IServiceLogBook serviceLogBook)
        {
            this._serviceUser = serviceUser;
            this._serviceLogin = serviceLogin;
            this._serviceLogBook = serviceLogBook;
        }

        [HttpGet]
        public IActionResult Index()
        {

            if (HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home", new { HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value });
            return View();
        }

        [HttpGet("[action]")]
        public IActionResult AccesDenied() => View();

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
                {
                    return RedirectToAction("LoginValidation", "Login", new { userName });
                }
                LogBook logBook = LogBook.Create("HomeController", "Index", "Test1");
                this._serviceLogBook.SaveInformationLog(logBook);
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
            if (HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home", new { HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value });
            UserModel user = new UserModel();
            user.UserName = userName;
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginValidation(UserModel user)
        {
            try
            {
                string errorMessage = string.Empty;
                DtoLogin login = new DtoLogin() { UserName = user.UserName, Password = user.Password };

                if (!ModelState.IsValid)
                    return View("LoginValidation", new UserModel { UserName = user.UserName });

                DtoLoginResponse userLogin = _serviceLogin.Login(login);

                if (userLogin.StatusLogin == StatusLogin.Ok)
                {
                    await SignIn(userLogin);
                    return RedirectToAction("Index", "Home", new { user.UserName });
                }

                switch (userLogin.StatusLogin)
                {
                    case StatusLogin.UserOrPasswordIncorrect:
                        errorMessage = "Password Incorrect.";
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
            catch (Exception ex)
            {
                ModelState.AddModelError("UserName", $"An error occurred while searching for the information of user, ticket:{SaveErrror(ex)}");
            }

            return View("LoginValidation", new UserModel { UserName = user.UserName });
        }

        [HttpGet]
        public async Task<IActionResult> Singout()
        {

            try
            {
                Guid userId = Guid.Parse(HttpContext.Session.GetString("userId"));
                _serviceLogin.SignOut(userId);
                HttpContext.Session.Remove("userId");
                await HttpContext.SignOutAsync();
                

            }
            catch (Exception ex)
            {
                Guid error = SaveErrror(ex);
                return View("Error", new ErrorViewModel() { RequestId = error });
            }

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
                    userViewModel.UserName, userViewModel.LastName, userViewModel.Age,
                    string.Empty, userViewModel.Email, Guid.NewGuid());

                _serviceUser.CreateUser(userFkw, false);

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
