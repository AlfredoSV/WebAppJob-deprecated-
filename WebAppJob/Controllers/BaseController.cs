using DocumentFormat.OpenXml.Office2010.Excel;
using Domain.Entities;
using Framework.Security2023.Dtos;
using Framework.Utilities202.Entities;
using Framework.Utilities2023.IServices;
using Framework.Utilities2023.Log.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using NLog;
using NLog.Web;
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
            Claim[] claims = new Claim[3];

            claims[0] = new Claim(ClaimTypes.Name, dtoLogin.User.UserName);
            claims[1] = new Claim(ClaimTypes.NameIdentifier, dtoLogin.User.Id.ToString());
            claims[2] = new Claim(ClaimTypes.Role, "Admin");

            claimsIdentity.AddClaims(claims);
            claimsPrincipal.AddIdentity(claimsIdentity);
            HttpContext.Session.SetString("userId", dtoLogin.User.Id.ToString());
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
            logBook.IdName = (Guid)idError;
            this.Log.SaveErrorLog(logBook);

            return (Guid)idError;
        }
    }
}
