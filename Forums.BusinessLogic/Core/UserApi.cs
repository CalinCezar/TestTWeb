using Forums.BusinessLogic.DBModel;
using Forums.Domain.Entities.Response;
using Forums.Domain.Entities.User;
using Forums.Domain.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Validation;
using System.Configuration;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Data;
using System.Runtime.Remoting.Messaging;

namespace Forums.BusinessLogic.Core
{
    public class UserApi
    {
        
        internal GeneralResp UserAuthLogic(ULoginData data)
        {
                //SQL connect, select, check data, logic on data
            UDbTable user;
            using (var db = new UserContext())
            {
                user = db.Users.FirstOrDefault(u => (u.Username == data.Credential || u.Email == data.Credential) && u.Password == data.Password);
            }

            if(user == null) return new GeneralResp { Status = false, StatusMsg="Invalid Credentials"};
            return new GeneralResp { Status = true };
        }
        internal GeneralResp RegisterUserAction(URegisterData data)
        {
            var newUser = new UDbTable()
            {
                Username = data.Credential,
                Password = data.Password,
                Email = data.Email,
                InfoBlog = data.InfoBlog,
                LastLogin = data.LoginDateTime,
                LasIP = data.LoginIp,
                Level = UserRole.User
     
            };
            // CONNECT WITH DB
            using (var db = new UserContext())
            {
                db.Users.Add(newUser);
                db.SaveChanges();
            }
            
            return new GeneralResp { Status = true};
        }
    }
}
