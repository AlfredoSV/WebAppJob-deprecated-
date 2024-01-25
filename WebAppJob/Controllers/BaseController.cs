using Domain.Entities;
using Framework.Security2023.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using NLog.Web;
using System.Security.Claims;

namespace WebAppJob.Controllers
{
    public class BaseController : Controller
    {
        public readonly Logger _logger;

        public BaseController()
        {
            _logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
        }

        protected void SignIn(DtoLoginResponse dtoLogin)
        {

            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();
            ClaimsIdentity claimsIdentity = new ClaimsIdentity();
            Claim[] claims = new Claim[2];

            claims[0] = new Claim("userName", dtoLogin.User.UserName);
            claims[0] = new Claim("id", dtoLogin.User.Id.ToString());

            claimsIdentity.AddClaims(claims);
            claimsPrincipal.AddIdentity(claimsIdentity);
            HttpContext.Session.SetString("userId", dtoLogin.User.Id.ToString());
            HttpContext.SignInAsync(claimsPrincipal);
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
                err
                = $"{ex.Message}-{ex.Source}"
            });
        }
        protected Guid? SaveErrror(Exception ex, Guid? idError = null)
        {
            if(idError == null)
                idError = Guid.NewGuid();

            if (ex is CommonException)
            {
                _logger.Error($"{idError}:{ex.Message}-{ex.Source}");

                return idError;
            }

            _logger.Fatal($"{idError}:{ex}");

            return idError;

        }


    }
}
