using Framework.Security2023.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAppJob.Controllers
{
    public class BaseController : Controller
    {

        protected void SignIn(DtoLoginResponse dtoLogin)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity();
            Claim[] claims = new Claim[3];
            claims[0] = new Claim("",dtoLogin.UserName);
            claims[0] = new Claim("", dtoLogin.User.Id.ToString());
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();
                      
            claimsIdentity.AddClaims(claims);
            claimsPrincipal.AddIdentity(claimsIdentity);
            HttpContext.Session.SetString("userId", dtoLogin.User.Id.ToString());
            HttpContext.SignInAsync(claimsPrincipal);
        }

    }
}
