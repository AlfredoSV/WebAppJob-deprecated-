using Application.IServices;
using Framework.Security2023;
using Framework.Security2023.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Web;
namespace WebAppJob.Filters
{
	public class AuthFilter : ActionFilterAttribute,IAuthorizationFilter
	{
		public IServiceUser _serviceUser;
		private string _role;

		public AuthFilter(string role)
		{
			_role = role;
			_serviceUser = new ServiceUser();
		}

		public void OnAuthorization(AuthorizationFilterContext context)
		{
			string idStr = context.HttpContext.Session.GetString("idUser")??"";
			Guid id;
			bool userExist = false;
			UserFkw user = null;

			//Validate the id
			if(Guid.TryParse(idStr, out id))
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
			else
			{
				//Validate the Role
				if (user.Role is null &&
					user.Role.NameRol.ToUpper().Equals(_role.ToUpper()))
				{
					context.Result = new RedirectToRouteResult(new RouteValueDictionary
					{{ "Controller", "Home" },
					{ "Action", "NoAuthorization" } });

					context.Result.ExecuteResultAsync(context);
				}
			}

		}
	}
}
