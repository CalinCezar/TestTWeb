using Forums.Domain.Entities.Response;
using Forums.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Forums.BusinessLogic.Interfaces
{
    public interface ISession
    {
        GeneralResp UserPassCheckAction(ULoginData data);
        GeneralResp RegisterNewUserAction(URegisterData data);
        HttpCookie GenCookie(string loginCredential);
        UserMinimal GetUserByCookie(string apiCookieValue);
        GeneralResp SendEmailToUserAction(string email, string name, string subject, string body);
        GeneralResp ResetPasswordAction(string email, string password);
        GeneralResp ExistingEmailInDB(string email);
    }
}

// interfata - contract care specifica ce trebuie sa faca entitatea.