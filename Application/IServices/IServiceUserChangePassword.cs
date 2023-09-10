using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IServiceUserChangePassword
    {
        void SendEmailForChangePassword(Guid userId);
        void ChangePassword(Guid userId, string password);
    }
}
