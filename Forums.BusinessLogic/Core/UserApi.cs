using Forums.BusinessLogic.DBModel;
using Forums.Domain.Entities.Response;
using Forums.Domain.Entities.User;
using Forums.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forums.BusinessLogic.Core
{
    public class UserApi
    {
        internal GeneralResp UserAuthLogic(ULoginData data)
        {
            UDbTable user;
            using (var db = new UserContext())
            {
                user = db.Users.FirstOrDefault(u => u.Username == data.Credential);
            }
                //SQL connect, select, check data, logic on data


                return new GeneralResp { Status = false, StatusMsg = "" };
        }
        internal GeneralResp RegisterUserAction(URegisterData data)
        {
            var newUser = new UDbTable()
            {
                Username = data.Credential,
                Password = data.Password,
                Email = data.Email,
                InfoBlog = data.InfoBlog,
                Level = UserRole.User
            };
            // CONNECT WITH DB
            using (var db = new UserContext())
            {
                db.Users.Add(newUser);
                db.SaveChanges();
            }

                return new GeneralResp();
        }
    }
}
