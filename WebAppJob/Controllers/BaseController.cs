using Domain.Entities;
using Framework.Security2023.Dtos;
using Framework.Utilities.Entities;
using Framework.Utilities.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAppJob.Controllers
{
    public class BaseController : Controller
    {
        public IServiceLogBook Log { get; set; }

        public BaseController(IServiceLogBook log)
        {
            this.Log = log;
        }

        protected async Task SignIn(DtoLoginResponse dtoLogin)
        {
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();
            ClaimsIdentity claimsIdentity = new ClaimsIdentity("CookieAuth");
            Claim[] claims =
            [
                new Claim(ClaimTypes.Name, dtoLogin.User.UserName),
                new Claim(ClaimTypes.NameIdentifier, dtoLogin.User.Id.ToString()),
                new Claim(ClaimTypes.Role, dtoLogin.User.Role.RolName),
            ];

            claimsIdentity.AddClaims(claims);
            claimsPrincipal.AddIdentity(claimsIdentity);
            //HttpContext.Response.Cookies.Append()
            HttpContext.Session.SetString("userId", dtoLogin.User.Id.ToString());
            HttpContext.Session.SetString("userName", dtoLogin.User.UserName);
            await HttpContext.SignInAsync(claimsPrincipal);
        }

        protected ObjectResult ReturnResponseIncorrect(Exception ex)
        {
            Guid idError = Guid.NewGuid();
            SaveErrror(ex, idError);
            return StatusCode(500, new
            {
                err
                = "Ocurrio un error inesperado, favor de revisar este ticket con soporte:" + idError
            });
        }

        protected ObjectResult ReturnResponseErrorCommon(CommonException ex)
        {
            Guid idError = Guid.NewGuid();
            SaveErrror(ex, idError);
            return StatusCode(400, new
            {
                err = $"{ex.Message}-{ex.Source}"
            });
        }

        protected Guid SaveErrror(Exception ex, Guid? idError = null)
        {
            idError = idError == null ? Guid.NewGuid() : idError;
            LogBook logBook = LogBook.Create(nameof(BaseController), nameof(SaveErrror), $"{ex.Message}-{ex.InnerException}");
            logBook.Id = (Guid)idError;
            this.Log.SaveErrorLog(logBook);

            return (Guid)idError;
        }
    }
}
