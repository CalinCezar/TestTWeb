using Forums.BusinessLogic.Core;
using Forums.BusinessLogic.Interfaces;
using Forums.Domain.Entities.Response;
using Forums.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Forums.BusinessLogic
{
    public class SessionBL : UserApi, ISession
    {
        public GeneralResp UserPassCheckAction(ULoginData data)
        {
            return UserAuthLogic(data);
        }
        public GeneralResp RegisterNewUserAction(URegisterData data)
        {
            return RegisterUserAction(data); 
        }
        public HttpCookie GenCookie(string loginCredential)
        {
            return Cookie(loginCredential);
        }

        public UserMinimal GetUserByCookie(string apiCookieValue)
        {
            return UserCookie(apiCookieValue);
        }
    }
}
