using Forums.Domain.Entities.Response;
using Forums.Domain.Entities.User;
using Forums.Domain.Entities.User.DbModel;
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

            //SQL connect, select, check data, logic on data


            //return new GeneralResp();
            return new GeneralResp { Status = false, StatusMsg = null };
        }
        internal GeneralResp RegisterUserAction(URegisterData data)
        {
            var newUser = new User()
            {

            };
            return new GeneralResp();
        }
        //internal GeneralResp RegisterUserAction(URegisterData data)
        //{
        //var newUser = new User()
        //{
        // Credential = data.Credential;
        // Password = data.Password
        // Email = data.Email;
        // Level = UserRole.User
        //}
        // connection with DataBase
        // using (var db = new UDbTable())
        //{
        //db.Users.Add(newUser);
        // db.SaveChanges();
        //}
        //return new GeneralResp();
        //}
    }
}
