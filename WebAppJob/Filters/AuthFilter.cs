using Application.IServices;
using Framework.Security2023;
using Framework.Security2023.Entities;
using Framework.Security2023.IServices;
using Framework.Security2023.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Web;
namespace WebAppJob.Filters
{
	public class AuthFilter : ActionFilterAttribute,IAuthorizationFilter
	{
		public IServiceUser _serviceUser;
		private string _role;

		public AuthFilter()
		{
			_role = string.Empty;
			_serviceUser = new ServiceUser();
		}

		public void OnAuthorization(AuthorizationFilterContext context)
		{
			
			Guid id = Guid.Empty;
			bool userExist = false;		
            string idStr = context.HttpContext.Session.GetString("idUser") ?? "";
            UserFkw user;

            if (Guid.TryParse(idStr, out id))
			{
				user = _serviceUser.GetUserById(id);
				userExist =  user != null;
			}
				
			if(!userExist)
			{
				context.Result = new RedirectToRouteResult(new RouteValueDictionary
					{{ "Controller", "Login" },
					{ "Action", "Index" } });
					context.Result.ExecuteResultAsync(context);
			}


		}
	}
}
