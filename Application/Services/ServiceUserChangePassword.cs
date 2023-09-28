using Application.IServices;
using Framework.Security2023;
using Framework.Security2023.IServices;

namespace Application.Services
{
    public class ServiceUserChangePassword : IServiceUserChangePassword
    {
        private readonly IServiceUser _serviceUser;

        public ServiceUserChangePassword(IServiceUser serviceUser)
        {
            _serviceUser = serviceUser;

        }
        public void ChangePassword(Guid userId, string password)
        {
            _serviceUser.UpdatePassword(userId, password);
            
        }

        public void SendEmailForChangePassword(Guid userId)
        {
           
        }
    }
}
